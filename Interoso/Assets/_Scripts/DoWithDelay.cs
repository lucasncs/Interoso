using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoWithDelay : MonoBehaviour
{
	public float delay;
	public UnityEvent Execute;


	void Start()
	{
		this.Invoke(Execute.Invoke, delay);
	}
}
