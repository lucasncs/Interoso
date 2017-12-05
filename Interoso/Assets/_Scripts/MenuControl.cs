using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
	public GameObject pauseMenu;

	public AudioSource _source;
	public AudioEvent hoverButton;

	private bool isPaused;

	private void Start()
	{
		UnPauseGame();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (!isPaused)
				PauseGame();
			else
				UnPauseGame();
		}
	}

	public void ChangeScene(string lvlName)
	{
		Fade.Initialize(Color.black, 1, lvlName);
	}

	public void PauseGame()
	{
		isPaused = true;
		Time.timeScale = 0;
		if (pauseMenu != null) pauseMenu.SetActive(true);
	}

	public void UnPauseGame()
	{
		isPaused = false;
		Time.timeScale = 1;
		if (pauseMenu != null) pauseMenu.SetActive(false);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		Fade.Initialize(Color.black, 5, () => UnityEditor.EditorApplication.isPlaying = false);
#else
		Fade.Initialize(Color.black, 5, Application.Quit);
#endif
	}

	public void PlayHover()
	{
		hoverButton.Play(_source);
	}
}
