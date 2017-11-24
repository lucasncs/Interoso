using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
	public int rotationOffset = 90;
	public Vector2 clampRotation;
	public bool invert;

	private Animator _anim;
	
	void Start()
	{
		_anim = GetComponent<Animator>();
	}

	public void Aim(int dir)
	{
		_anim.SetTrigger("Attack");



		Vector3 difference = !invert ? (dir > 0 ? Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position : transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition))
			: (dir < 0 ? transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) : Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
		difference.Normalize();

		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		rotZ = Mathf.Clamp(rotZ, clampRotation.y, clampRotation.x);

		transform.rotation = Quaternion.Euler(0, 0, rotZ + rotationOffset);
	}
}
