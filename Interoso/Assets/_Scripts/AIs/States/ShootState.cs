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
		machine.LookToPlayer();
		machine.StopMoving();

		machine.Animation.AttackState = true;

		timer = 0;
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
		machine.Animation.AttackState = true;


		if (!machine.InRangeForDetection())
		{
			machine.SetState(machine.PatrolState);
		}
	}

	public override void OnStateExit()
	{
		machine.Attack(false);
		machine.Animation.AttackState = false;

		timer = 0;
	}
}