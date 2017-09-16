using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneretorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		LevelGenerator myTarget = (LevelGenerator)target;

		bool _lock = myTarget.transform.childCount <= 0;

		GUI.enabled = _lock;

		if (GUILayout.Button("Load Level"))
		{
			myTarget.GenerateLevel();
		}

		GUI.enabled = !_lock;

		if (GUILayout.Button("UnLoad Level"))
		{
			myTarget.EmptyLevel();
		}

		GUILayout.Space(10);

		GUI.enabled = true;
		DrawDefaultInspector();
	}
}
