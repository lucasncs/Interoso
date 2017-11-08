using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seven.StateMachine;

public class ShootState : State<EnemyStateMachine>
{
	public ShootState(EnemyStateMachine machine) : base(machine) { }

	private float timer = 0;

	public override void OnStateEnter()
	{
		machine.GetComponentInChildren<SpriteRenderer>().color = Color.red;
		machine.LookToPlayer();
		machine.StopMoving();
	}

	public override void Tick()
	{
		// Shooting logic
		timer += Time.deltaTime;
		if (timer >= machine.fireRate)
		{
			machine.Attack();
			timer = 0;
		}

		machine.LookToPlayer();


		if (!machine.InRangeForDetection())
		{
			machine.SetState(machine.PatrolState);
		}
	}

	public override void OnStateExit()
	{
		timer = 0;
	}
}