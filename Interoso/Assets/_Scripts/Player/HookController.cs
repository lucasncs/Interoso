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
	private float maxLenght = 5f;
	[SerializeField]
	private float minLenght = 1.5f;
	public LayerMask mask;
	public LineRenderer visual;

	public bool isHooked { get; private set; }

	void Start()
	{
		_hook = GetComponent<DistanceJoint2D>();
		_hook.distance = maxLenght;
		cam = Camera.main;

		Stop();
	}

	void FixedUpdate()
	{
		if (isHooked)
		{
			visual.SetPosition(0, transform.position);
		}
	}

	private bool Enabled
	{
		set
		{
			isHooked = _hook.enabled = visual.enabled = value;
        }
	}

	public void UseHook()
	{
		bool canHook = Hook();
		Enabled = canHook;

		if (canHook)
		{
			//var anchor = hit.point - new Vector2(hit.transform.position.x, hit.transform.position.y);
			_hook.connectedAnchor = hit.point;

			var newDist = Vector2.Distance(transform.position, hit.point);
			//_hook.distance = newDist < _hook.distance ? newDist >= minLenght ? newDist : minLenght : maxLenght;
			//_hook.distance = newDist - 1;
			_hook.distance = Mathf.Clamp(newDist, minLenght, maxLenght);

			visual.SetPosition(0, transform.position);
			visual.SetPosition(1, hit.point);
		}
	}

	private bool Hook()
	{
		targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
		targetPos.z = 0;
		hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

		return hit.collider != null;
    }
	
	public void Stop()
	{
		isHooked = false;
		Enabled = false;
	}
}
