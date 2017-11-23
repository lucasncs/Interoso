using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
	public void ChangeScene(string lvlName)
	{
		Fade.Initialize(Color.black, 1, lvlName);
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1;
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		Fade.Initialize(Color.black, 5, () => UnityEditor.EditorApplication.isPlaying = false);
#else
		Fade.Initialize(Color.black, 5, Application.Quit);
#endif
	}
}
