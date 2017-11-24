using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController2D : PlayerController2D
{

	protected override void Start()
	{
		_stats = GetComponent<EnemyStats>();

		_stats.OnTakeDamage += OnTakeDamage;
		//base.Start();
	}

	private void LateUpdate()
	{
		_motor.normalizedXMovement *= -1;
	}

}
