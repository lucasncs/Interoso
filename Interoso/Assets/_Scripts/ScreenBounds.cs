using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
	private static Vector3 min;
	private static Vector3 max;

	public static Vector3 Min
	{
		get
		{
			return min;
		}
	}

	public static Vector3 Max
	{
		get
		{
			return max;
		}
	}


	static Vector3 bottomLeft;
	static Vector3 topRight;

	public static Rect cameraRect;


	private void SetValues()
	{
		min = Camera.main.ScreenToWorldPoint(Vector3.zero);
		max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

		bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

		cameraRect = new Rect(
			bottomLeft.x,
			bottomLeft.y,
			topRight.x - bottomLeft.x,
			topRight.y - bottomLeft.y
			);
	}

	private void Awake()
	{
		SetValues();
	}

	private void Update()
	{
		SetValues();
	}
}
