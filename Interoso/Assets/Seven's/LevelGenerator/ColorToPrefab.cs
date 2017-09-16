using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Level Generator/Tile")]
public class ColorToPrefab : ScriptableObject
{
	[SerializeField]
	protected Color _color;
	[SerializeField]
	protected GameObject _prefab;
	[SerializeField]
	protected bool _firstSibling;

	public Color color
	{
		get
		{
			return _color;
		}
	}

	public virtual GameObject prefab
	{
		get
		{
			return _prefab;
		}
	}

	public bool firstSibling
	{
		get
		{
			return _firstSibling;
		}
	}
}