using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StatsController
{
	protected override void Death()
	{
		base.Death();
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.CompareTag("EnemyShot"))
		{
			Damage(10);
			hit.GetComponent<BulletDestroyScript>().Destroy();
		}

		if (hit.gameObject.CompareTag("EnemyMelee"))
		{
			Damage(20);
			hit.GetComponent<BulletDestroyScript>().Destroy();
		}
	}
}
