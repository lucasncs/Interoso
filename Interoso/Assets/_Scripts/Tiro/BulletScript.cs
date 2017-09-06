using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float speed = 5;
	
	void Update()
	{
		transform.Translate(0, speed * Time.deltaTime, 0);
	}
}