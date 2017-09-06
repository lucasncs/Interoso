using UnityEngine;

public class ScreenBounds
{
	private static Vector3 min = Camera.main.ScreenToWorldPoint(Vector3.zero);
	private static Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

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


	static Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
	static Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(
		Camera.main.pixelWidth, Camera.main.pixelHeight));

	public static Rect cameraRect = new Rect(
		bottomLeft.x,
		bottomLeft.y,
		topRight.x - bottomLeft.x,
		topRight.y - bottomLeft.y);
}
