#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public Texture2D map;

	public ColorToPrefab[] colorMappings;

	void Awake()
	{
		if (transform.childCount <= 0)
			GenerateLevel();
	}

	public void GenerateLevel()
	{
		//EmptyLevel();

        for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile(int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);

		if (pixelColor.a == 0)
		{
			// The pixel is transparrent. Let's ignore it!
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor) && colorMapping.prefab != null)
			{
				Vector2 position = new Vector2(x, y);
				#if UNITY_EDITOR
					GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(colorMapping.prefab);
					go.transform.position = position;
					go.transform.localRotation = Quaternion.identity;
					go.transform.parent = transform;
					if (colorMapping.firstSibling) go.transform.SetAsFirstSibling();
					else go.transform.SetAsLastSibling();
				#else
					Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
				#endif
			}
		}
	}

	public void EmptyLevel()
	{
		// Find all children and eliminate them.

		while (transform.childCount > 0)
		{
			Transform c = transform.GetChild(0);
			c.SetParent(null);
			#if UNITY_EDITOR
				DestroyImmediate(c.gameObject);
			#else
				Destroy(c.gameObject);
			#endif
		}
	}
}
