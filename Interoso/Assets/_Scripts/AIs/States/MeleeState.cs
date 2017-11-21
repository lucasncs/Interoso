using System.Collections;
using System.Collections.Generic;
using Seven.StateMachine;
using UnityEngine;

public class MeleeState : State<EnemyStateMachine>
{
	public MeleeState(EnemyStateMachine machine) : base(machine) { }

	private float timer = 0;

	public override void OnStateEnter()
	{
		machine.LookToPlayer();

		timer = machine.fireRate;
	}

	public override void Tick()
	{
		if (machine.InRangeToAttack())
		{
			machine.Animation.AttackState = true;
			machine.StopMoving();

			timer += Time.deltaTime;
			if (timer >= machine.fireRate)
			{
				machine.Attack();
				timer = 0;
			}
		}
		else
		{
			machine.Animation.AttackState = false;
			machine.Move(machine.player.transform.position, machine.walkSpeed * 2.5f);
		}

		machine.LookToPlayer();

		if (!machine.InRangeForDetection())
		{
			machine.SetState(machine.PatrolState);
		}
	}

	public override void OnStateExit()
	{
		machine.Attack(false);
		machine.Animation.AttackState = false;
	}
}
