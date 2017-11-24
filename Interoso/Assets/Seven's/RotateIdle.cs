using UnityEngine;
using System.Collections;

public class RotateIdle : MonoBehaviour
{

	public int X = 0;
	public int Y = 0;
	public int Z = 0;


	void Update()
	{
		transform.Rotate(X * Time.deltaTime, Y * Time.deltaTime, Z * Time.deltaTime);
	}
}
