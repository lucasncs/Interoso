using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	[SerializeField]
	protected GameObject shot;
	[SerializeField]
	protected Transform[] muzzles;

	void Start()
	{
		if (shot == null)
			Debug.LogError(transform.name + "\nShot is EMPTY", gameObject);
	}

	public virtual void Shoot(int dir = 1)
	{
		float sign = Mathf.Sign(dir);

		if (muzzles.Length == 0)
		{
			var s = GoShot(transform.position);
			s.GetComponent<BulletScript>().speed *= sign;
		}
		else
		{
			foreach (Transform muzzle in muzzles)
			{
				var s = GoShot(muzzle.position, muzzle.rotation);
				s.GetComponent<BulletScript>().speed *= sign;
			}
		}
	}

	protected GameObject GoShot(Vector2 pos)
	{
		return GoShot(pos, Quaternion.identity);
	}
	protected GameObject GoShot(Vector2 pos, Quaternion rot)
	{
		return Instantiate(shot, pos, rot);
	}
}