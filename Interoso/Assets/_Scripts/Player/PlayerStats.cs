using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seven.Stats;

public class PlayerStats : StatsController<StatWithBar>
{
	protected override void Awake()
	{
		//base.Awake();
		health.Initialize();
	}

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

		if (hit.gameObject.CompareTag("Life"))
		{
			health.Value = health.MaxVal;
			Destroy(hit.gameObject);
		}
	}
}
