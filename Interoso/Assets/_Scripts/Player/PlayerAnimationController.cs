using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
	private HookController _hook;

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
