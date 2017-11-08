using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterStateMachine : EnemyStateMachine
{
	private Shooter _shot;

	private void Awake()
	{
		PatrolState = new PatrolState(this);
		AttackState = new ShootState(this);

		_shot = GetComponent<Shooter>();
	}

	public override void Attack()
	{
		_shot.Shoot(FaceDirection);
	}
}
