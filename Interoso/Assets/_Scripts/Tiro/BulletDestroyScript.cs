using UnityEngine;

public class BulletDestroyScript : MonoBehaviour
{
	private Coroutine destroyCoroutine;
	public float destroyTime = 2;
	
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
		gameObject.SetActive(false);
    }

	void OnDisable()
	{
		StopCoroutine(destroyCoroutine);
	}
}