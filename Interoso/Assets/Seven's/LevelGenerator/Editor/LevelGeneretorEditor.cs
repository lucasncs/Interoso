using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneretorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		LevelGenerator myTarget = (LevelGenerator)target;

		GUI.enabled = !Application.isPlaying;

		if (GUILayout.Button("Load Level"))
		{
			myTarget.GenerateLevel();
		}

		if (GUILayout.Button("UnLoad Level"))
		{
			myTarget.EmptyLevel();
		}

		GUILayout.Space(10);

		DrawDefaultInspector();
	}
}
