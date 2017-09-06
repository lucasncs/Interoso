using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seven.StateMachine;
using System;

public class ShootState : State<EnemyStateMachine>
{
	public ShootState(EnemyStateMachine machine) : base(machine) { }

	public override void OnStateEnter()
	{
		machine.GetComponentInChildren<SpriteRenderer>().color = Color.red;
		machine.LookToPlayer();
		machine.StopMoving();
	}

	public override void Tick()
	{
		// Shooting logic

		if (!machine.InRangeForDetection())
		{
			machine.SetState(new PatrolState(machine));
		}
	}
}