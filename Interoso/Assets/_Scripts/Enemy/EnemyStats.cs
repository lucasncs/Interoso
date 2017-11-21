using System;
using System.Collections;
using System.Collections.Generic;
using Seven.Stats;
using UnityEngine;

public class EnemyStats : StatsController<Stat>
{
	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.CompareTag("PlayerShot"))
		{
			Damage(20);
			//hit.GetComponent<BulletDestroyScript>().Destroy();
		}
	}
}
