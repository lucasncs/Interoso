using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : StatsController
{
	protected override void Death()
	{
		base.Death();
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.CompareTag("PlayerShot"))
		{
			Damage(20);
			hit.GetComponent<BulletDestroyScript>().Destroy();
		}
	}
}
