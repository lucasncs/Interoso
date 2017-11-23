using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
	private HookController _hook;

	public float damageDuration = .5f;


	public bool Attack
	{
		set
		{
			if (value)
				_animator.SetTrigger("Attack");
			else
				_animator.ResetTrigger("Attack");
		}
	}

	public bool Walk
	{
		set
		{
			_animator.SetBool("Walk", value);
		}
	}

	public bool Jump
	{
		set
		{
			_animator.SetBool("Jump", value);
		}

		get
		{
			return _animator.GetBool("Jump");
		}
	}

	//public bool AttackState
	//{
	//	set
	//	{
	//		if (value)
	//			_animator.SetTrigger("AttackState");
	//		else
	//			_animator.ResetTrigger("AttackState");
	//	}
	//}

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

	protected override void Start()
	{
		base.Start();
		_hook = GetComponent<HookController>();
	}

	protected override bool TurnCondictions
	{
		get
		{
			return base.TurnCondictions || _hook.isHooked;
		}
	}
}
