using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Random", menuName = "Level Generator/Tile Random")]
public class ColorToRandomPrefab : ColorToPrefab
{
	[SerializeField]
	private GameObject[] randomPrefabs;

	public override GameObject prefab
	{
		get
		{
			return randomPrefabs[Random.Range(0, randomPrefabs.Length)];
		}
	}
}
