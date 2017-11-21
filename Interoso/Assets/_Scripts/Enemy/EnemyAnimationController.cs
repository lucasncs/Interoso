using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : AnimationController
{
	public float damageDuration = .5f;


	public bool Attack
	{
		set
		{
			_animator.SetBool("Attack", value);
		}
	}

	public bool AttackState
	{
		set
		{
			if (value)
				_animator.SetTrigger("AttackState");
			else
				_animator.ResetTrigger("AttackState");
		}
	}

	private bool damage;
	public bool Damage
	{
		set
		{
			if (value)
				_animator.SetTrigger("Damage");
			else
				_animator.ResetTrigger("Damage");
			damage = value;
		}

		get
		{
			return damage;
		}
	}
}
