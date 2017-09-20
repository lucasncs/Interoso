using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public Vector2 speed;

	void Update()
	{
		transform.Translate(speed * Time.deltaTime);
	}
}