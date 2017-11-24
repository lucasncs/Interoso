using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FaderScript : MonoBehaviour
{
	public bool start = false;
	public float fadeDamp = 0.0f;
	public string sceneToLoad;
	public float alpha = 0.0f;
	public Color fadeColor;
	public bool isFadeIn = false;
	public Action execWhenDone = null;

	public bool isSwitchScenes;

	private Texture2D myTex;

	private AsyncOperation async;

	private void Start()
	{
		myTex = new Texture2D(1, 1);
		myTex.SetPixel(0, 0, fadeColor);
		myTex.Apply();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnGUI ()
	{
		if (!start) return;
		isSwitchScenes = execWhenDone != null ? false : true;

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);


		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), myTex);

		if (isFadeIn)
			alpha = Mathf.Lerp (alpha, -0.1f, fadeDamp * Time.deltaTime);
		else
			alpha = Mathf.Lerp (alpha, 1.1f, fadeDamp * Time.deltaTime);

		if (alpha >= 1 && !isFadeIn) //Fade OUT - 0.5
		{
			if (execWhenDone != null) execWhenDone(); //Executa as acoes
			else
			{
				if (async == null)
					async = SceneManager.LoadSceneAsync(sceneToLoad);
			}
			DontDestroyOnLoad(gameObject);
			if (!isSwitchScenes) isFadeIn = true;
		}
		else if (alpha <= 0 && isFadeIn) //Fade IN - 1
		{
			Destroy(gameObject);		
		}
	}

	private void OnSceneLoaded (Scene scene, LoadSceneMode loadSceneMode)
	{
		isFadeIn = true;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}