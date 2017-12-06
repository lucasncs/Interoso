/// <summary>
/// Handles parsing and execution of console commands, as well as collecting log output.
/// Copyright (c) 2014-2015 Eliot Lash
/// </summary>

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Seven.Console
{
	public delegate void CommandHandler(string[] args);

	internal class ConsoleController
	{
		#region Event declarations
		// Used to communicate with ConsoleView
		public delegate void LogChangedHandler(string[] log);
		public event LogChangedHandler OnLogChanged;

		public delegate void VisibilityChangedHandler(bool visible);
		public event VisibilityChangedHandler OnVisibilityChanged;
		#endregion

		/// <summary>
		/// Object to hold information about each command
		/// </summary>
		class CommandRegistration
		{
			public string command { get; private set; }
			public CommandHandler handler { get; private set; }
			public string help { get; private set; }

			public CommandRegistration(string command, CommandHandler handler, string help)
			{
				this.command = command;
				this.handler = handler;
				this.help = help;
			}
		}

		/// <summary>
		/// How many log lines should be retained?
		/// Note that strings submitted to appendLogLine with embedded newlines will be counted as a single line.
		/// </summary>
		const int scrollbackSize = 30;

		Queue<string> scrollback = new Queue<string>(scrollbackSize);
		List<string> commandHistory = new List<string>();
		Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

		public string[] Log { get; private set; } //Copy of scrollback as an array for easier use by ConsoleView

		const string repeatCmdName = "!!"; //Name of the repeat command, constant since it needs to skip these if they are in the command history

		public ConsoleController()
		{
			//When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
			//RegisterCommand("babble", babble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
			//RegisterCommand("echo", echo, "echoes arguments back as array (for testing argument parser)");
			RegisterCommand("help", help, "Print this help.");
			RegisterCommand("hide", hide, "Hide the console.");
			RegisterCommand(repeatCmdName, repeatCommand, "Repeat last command.");
			RegisterCommand("resetprefs", resetPrefs, "Reset & saves PlayerPrefs.");

			RegisterCommand("cls", cls, "Clears the console log.");
			RegisterCommand("immortal", immortal, "Turn the player immortal.");
			RegisterCommand("loadscene", loadScene, "Loads a scene by its index or name.");
			RegisterCommand("reload", reload, "Reload scene.");
			RegisterCommand("quit", quit, "Quits the application.");
		}

		void RegisterCommand(string command, CommandHandler handler, string help)
		{
			commands.Add(command, new CommandRegistration(command, handler, help));
		}

		public void AppendLogLine(string line)
		{
			Debug.Log(line);

			if (scrollback.Count >= ConsoleController.scrollbackSize)
			{
				scrollback.Dequeue();
			}
			scrollback.Enqueue(line);

			Log = scrollback.ToArray();
			if (OnLogChanged != null)
			{
				OnLogChanged(Log);
			}
		}

		public void RunCommandString(string commandString)
		{
			AppendLogLine("$ " + commandString);

			string[] commandSplit = ParseArguments(commandString);
			string[] args = new string[0];
			if (commandSplit.Length < 1)
			{
				AppendLogLine(string.Format("Unable to process command '{0}'", commandString));
				return;

			}
			else if (commandSplit.Length >= 2)
			{
				int numArgs = commandSplit.Length - 1;
				args = new string[numArgs];
				Array.Copy(commandSplit, 1, args, 0, numArgs);
			}
			RunCommand(commandSplit[0].ToLower(), args);
			commandHistory.Add(commandString);
		}

		public void RunCommand(string command, string[] args)
		{
			CommandRegistration reg = null;
			if (!commands.TryGetValue(command, out reg))
			{
				AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
			}
			else
			{
				if (reg.handler == null)
				{
					AppendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
				}
				else
				{
					reg.handler(args);
				}
			}
		}

		static string[] ParseArguments(string commandString)
		{
			LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
			bool inQuote = false;
			var node = parmChars.First;
			while (node != null)
			{
				var next = node.Next;
				if (node.Value == '"')
				{
					inQuote = !inQuote;
					parmChars.Remove(node);
				}
				if (!inQuote && node.Value == ' ')
				{
					node.Value = '\n';
				}
				node = next;
			}
			char[] parmCharsArr = new char[parmChars.Count];
			parmChars.CopyTo(parmCharsArr, 0);
			return (new string(parmCharsArr)).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}

		#region Command handlers
		//Implement new commands in this region of the file.

		/// <summary>
		/// A test command to demonstrate argument checking/parsing.
		/// Will repeat the given word a specified number of times.
		/// </summary>
		void babble(string[] args)
		{
			if (args.Length < 2)
			{
				AppendLogLine("Expected 2 arguments.");
				return;
			}
			string text = args[0];
			if (string.IsNullOrEmpty(text))
			{
				AppendLogLine("Expected arg1 to be text.");
			}
			else
			{
				int repeat = 0;
				if (!Int32.TryParse(args[1], out repeat))
				{
					AppendLogLine("Expected an integer for arg2.");
				}
				else
				{
					for (int i = 0; i < repeat; ++i)
					{
						AppendLogLine(string.Format("{0} {1}", text, i));
					}
				}
			}
		}

		void echo(string[] args)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string arg in args)
			{
				sb.AppendFormat("{0},", arg);
			}
			sb.Remove(sb.Length - 1, 1);
			AppendLogLine(sb.ToString());
		}

		void help(string[] args)
		{
			AppendLogLine("");
			foreach (CommandRegistration reg in commands.Values)
			{
				AppendLogLine(string.Format("{0}: {1}", reg.command, reg.help));
			}
		}

		void hide(string[] args)
		{
			if (OnVisibilityChanged != null)
			{
				OnVisibilityChanged(false);
			}
		}

		void repeatCommand(string[] args)
		{
			for (int cmdIdx = commandHistory.Count - 1; cmdIdx >= 0; --cmdIdx)
			{
				string cmd = commandHistory[cmdIdx];
				if (String.Equals(repeatCmdName, cmd))
				{
					continue;
				}
				RunCommandString(cmd);
				break;
			}
		}

		void cls(string[] args)
		{
			scrollback.Clear();
			Log = scrollback.ToArray();
			if (OnLogChanged != null)
			{
				OnLogChanged(Log);
			}
		}

		void quit(string [] args)
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		void reload(string[] args)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			AppendLogLine("Scene " + SceneManager.GetActiveScene().name + " Reloaded.");
			hide(null);
		}

		void loadScene(string[] args)
		{
			Action mainErrorMsg = ()=> AppendLogLine("syntax: loadScene ['index'/'name'] [scene index/name]");
			if (args.Length != 2)
			{
				AppendLogLine("Expected 2 arguments.");
				mainErrorMsg();
				return;
			}

			string type = args[0];

			if (string.IsNullOrEmpty(type))
			{
				AppendLogLine("Expected arg1 to be 'index' or 'name'.");
				mainErrorMsg();
			}
			else
			{
				if (type == "index")
				{
					int index = 0;
					if (!Int32.TryParse(args[1], out index))
					{
						AppendLogLine("Expected an integer for arg2.");
						mainErrorMsg();
					}
					else
					{
						SceneManager.LoadScene(index);
					}
				}
				else if (type == "name")
				{
					string name = args[1];
					if (string.IsNullOrEmpty(name))
					{
						AppendLogLine("Expected an string for arg2.");
						mainErrorMsg();
					}
					else
					{
						SceneManager.LoadScene(name);
					}
				}

				//SceneManager.sceneLoaded += (scene, mode)=> AppendLogLine("Scene " + scene.name + " was loaded.");

				hide(null);

			}
		}

		void immortal(string[] args)
		{
			CheatsManager.playerImortal = !CheatsManager.playerImortal;
			AppendLogLine(string.Format("The player {0} immortal now!", CheatsManager.playerImortal ? "IS" : "IS NOT"));
		}

		void resetPrefs(string[] args)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
			AppendLogLine("All PlayerPrefs deleted.");
		}

		#endregion
	}
}