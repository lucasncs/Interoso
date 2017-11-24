using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D bounds;

	public Transform target;

	private Camera thisCamera;
	private Vector3 min;
	private Vector3 max;

	void Start()
	{
		min = bounds.bounds.min;
		max = bounds.bounds.max;

		thisCamera = GetComponent<Camera>();

		if (target == null)
			target = GameObject.FindWithTag("Player").transform;
	}

	void LateUpdate()
	{
		float cameraHalfWidth = thisCamera.orthographicSize * ((float)Screen.width / Screen.height);
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(target.position.x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
		pos.y = Mathf.Clamp(target.position.y, min.y + thisCamera.orthographicSize, max.y - thisCamera.orthographicSize);

		transform.position = pos;
	}

	public void SetBounds(BoxCollider2D b)
	{
		bounds = b;
		min = bounds.bounds.min;
		max = bounds.bounds.max;
	}

	public void SetTarget(Transform t)
	{
		target = t;
	}
}
