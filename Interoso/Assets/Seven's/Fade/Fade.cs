using UnityEngine;
using System;

public static class Fade
{
	public static void Initialize(float _damp)
	{
		GameObject obj = new GameObject("Fader", typeof(FaderScript));
		FaderScript scr = obj.GetComponent<FaderScript>();
		scr.fadeDamp = _damp;
		scr.fadeColor = Color.white;
		scr.isSwitchScenes = false;
		scr.start = true; //permite que o fade comece
	}

	/// <summary>
	/// Starts the Fading
	/// </summary>
	/// <param name="_color">The fading color</param>
	/// <param name="_damp">Time of the fading (the higher the fastest)</param>
	public static void Initialize(Color _color, float _damp)
	{
		GameObject obj = new GameObject("Fader", typeof(FaderScript));
		FaderScript scr = obj.GetComponent<FaderScript>();
		scr.fadeDamp = _damp;
		scr.fadeColor = _color;
		scr.isSwitchScenes = false;
		scr.start = true; //permite que o fade comece
	}

	/// <summary>
	/// Starts the Fading between Scenes
	/// </summary>
	/// <param name="_color">The fading color</param>
	/// <param name="_damp">Time of the fading (the higher the fastest)</param>
	/// <param name="_scene">Scene to load</param>
	public static void Initialize(Color _color, float _damp, string _scene)
	{
		GameObject obj = new GameObject("Fader", typeof(FaderScript));
		FaderScript scr = obj.GetComponent<FaderScript>();
		scr.fadeDamp = _damp;
		scr.sceneToLoad = _scene;
		scr.fadeColor = _color;
		scr.isSwitchScenes = true;
		scr.start = true; //permite que o fade comece
	}

	/// <summary>
	/// Starts the Fading between Scenes
	/// </summary>
	/// <param name="_color">The fading color</param>
	/// <param name="_damp">Time of the fading (the higher the fastest)</param>
	/// <param name="_scene">Scene to load</param>
	public static void Initialize(Color _color, float _damp, int _scene)
	{
		//GameObject obj = new GameObject("Fader", typeof(FaderScript));
		//FaderScript scr = obj.GetComponent<FaderScript>();
		//scr.fadeDamp = _damp;
		//scr.sceneToLoad = scr.GetSceneName(_scene);
		//scr.fadeColor = _color;
		//scr.start = true; //permite que o fade comece

		Initialize(_color, _damp, UnityEngine.SceneManagement.SceneManager.GetSceneAt(_scene).name);
	}

	/// <summary>
	/// Starts the Fading
	/// </summary>
	/// <param name="_color">The fading color</param>
	/// <param name="_damp">Time of the fading (the higher the fastest)</param>
	/// <param name="_execWhenDone">Commands to execute when the fade ends (delegate)</param>
	public static void Initialize(Color _color, float _damp, params Action[] _execWhenDone)
	{
		GameObject obj = new GameObject("Fader", typeof(FaderScript));
		FaderScript scr = obj.GetComponent<FaderScript>();
		scr.fadeDamp = _damp;
		foreach (var exec in _execWhenDone) scr.execWhenDone += exec;
		scr.fadeColor = _color;
		scr.isSwitchScenes = false;
		scr.start = true; //permite que o fade comece
	}
}