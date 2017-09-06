using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	public GameObject shot;
	public Transform[] muzzles;
	public AudioClip shotSound;

	void Start()
	{
		if (shot == null)
			Debug.LogError(transform.name + "\nShot is EMPTY", gameObject);
	}

	public virtual void Shot()
	{
		if (muzzles.Length == 0)
		{
			GoShot(transform);
		}
		else
		{
			foreach (Transform muzzle in muzzles)
			{
				GoShot(muzzle);
			}
		}
	}

	protected void GoShot(Transform pos)
	{
		Instantiate<GameObject>(shot, pos.position, Quaternion.identity);
	}
}