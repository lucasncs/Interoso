/// <summary>
/// Marshals events and data between ConsoleController and uGUI.
/// Copyright (c) 2014-2015 Eliot Lash
/// </summary>

using UnityEngine;
using UnityEngine.UI;

namespace Seven.Console
{
	public class ConsoleView : MonoBehaviour
	{
		static bool alreadyHaveOne;

		ConsoleController console = new ConsoleController();

		bool didShow = false;

		public GameObject viewContainer;
		//public Canvas viewContainer;
		public Text logTextArea;
		public InputField inputField;
		[Tooltip("Stop time (pause game) when the console is displayed.")]
		public bool stopTime;

		private float currentTime;

		private void Awake()
		{
			if (!alreadyHaveOne)
			{
				alreadyHaveOne = true;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void Start()
		{
			if (console != null)
			{
				console.OnVisibilityChanged += OnVisibilityChanged;
				console.OnLogChanged += OnLogChanged;
			}
			UpdateLogStr(console.Log);

			if (stopTime)
				currentTime = Time.timeScale;

			SetVisibility(false);
		}

		~ConsoleView()
		{
			console.OnVisibilityChanged -= OnVisibilityChanged;
			console.OnLogChanged -= OnLogChanged;
		}

		private void Update()
		{
			//Toggle visibility when tilde key pressed
			if (Input.GetKeyUp("`"))
			{
				ToggleVisibility();
			}

			//Toggle visibility when 5 fingers touch.
			if (Input.touches.Length == 5)
			{
				if (!didShow)
				{
					ToggleVisibility();
					didShow = true;
				}
			}
			else
			{
				didShow = false;
			}
		}

		private void ToggleVisibility()
		{
			SetVisibility(!viewContainer.activeSelf);
			//SetVisibility(!viewContainer.enabled);
		}

		private void SetVisibility(bool visible)
		{
			viewContainer.SetActive(visible);
			//viewContainer.enabled = visible;

			inputField.text = "";

			if (visible)
			{
				SelectInputField();
			}

			if (visible && stopTime)
			{
				currentTime = Time.timeScale;
				Time.timeScale = 0;
			}
			else
				Time.timeScale = currentTime;
		}

		private void SelectInputField()
		{
			inputField.Select();
			inputField.ActivateInputField();
		}

		private void OnVisibilityChanged(bool visible)
		{
			SetVisibility(visible);
		}

		private void OnLogChanged(string[] newLog)
		{
			UpdateLogStr(newLog);
		}

		private void UpdateLogStr(string[] newLog)
		{
			if (newLog == null)
			{
				logTextArea.text = "";
			}
			else
			{
				logTextArea.text = string.Join("\n", newLog);
			}
		}

		/// <summary>
		/// Event that should be called by anything wanting to submit the current input to the console.
		/// </summary>
		public void RunCommand()
		{
			if (inputField.text == "" || inputField.text == "'") return;
			console.RunCommandString(inputField.text);
			inputField.text = "";
			SelectInputField();
		}

	}
}