using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
	private DistanceJoint2D _hook;

	private Vector3 targetPos;
	private RaycastHit2D hit;
	private Camera cam;

	[SerializeField]
	private float distance = 10f;
	[SerializeField]
	private float lenght = 5f;
	public LayerMask mask;

	public bool isHooked { get; private set; }

	void Start()
	{
		_hook = GetComponent<DistanceJoint2D>();
		_hook.enabled = false;
		_hook.distance = lenght;
		isHooked = false;
		cam = Camera.main;
	}

	void Update()
	{

	}

	public void Stop()
	{
		isHooked = _hook.enabled = false;
	}

	public void UseHook()
	{
		bool canHook = Hook();
		isHooked = _hook.enabled = canHook;

		if (canHook)
		{
			//var anchor = hit.point - new Vector2(hit.transform.position.x, hit.transform.position.y);
			_hook.connectedAnchor = hit.point;

			var newDist = Vector2.Distance(transform.position, hit.point);
			if (newDist < _hook.distance)
				_hook.distance = newDist;
			else
				_hook.distance = lenght;
        }
	}

	private bool Hook()
	{
		targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
		targetPos.z = 0;
		hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

		return hit.collider != null;
    }
}
