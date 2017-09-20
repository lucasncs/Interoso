using UnityEngine;

public class BulletDestroyScript : MonoBehaviour
{
	[SerializeField]
	private float destroyTime = 2;

	private Coroutine destroyCoroutine;
	
	void OnEnable()
	{
		destroyCoroutine = this.Invoke(Destroy, destroyTime);
	}

	void LateUpdate()
	{
		if (transform.position.x > ScreenBounds.Max.x || transform.position.x < ScreenBounds.Min.x ||
			transform.position.y > ScreenBounds.Max.y || transform.position.y < ScreenBounds.Min.y)
			Destroy();
	}

	public void Destroy()
	{
		//gameObject.SetActive(false);
		Destroy(gameObject);
    }

	void OnDisable()
	{
		StopCoroutine(destroyCoroutine);
	}

	//void OnTriggerEnter2D(Collider2D hit)
	//{
	//	if (hit != null)
	//	{
	//		Destroy();
	//	}
	//}
}