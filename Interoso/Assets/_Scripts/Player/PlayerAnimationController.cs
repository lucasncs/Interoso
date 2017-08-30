using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	public GameObject visualChild;

	private PlatformerMotor2D _motor;
	//private Animator _animator;
	//private bool _isJumping;
	private bool _currentFacingLeft;

	void Start()
	{
		_motor = GetComponent<PlatformerMotor2D>();
		//_animator = visualChild.GetComponent<Animator>();
		//_animator.Play("Idle");

		_motor.onJump += SetCurrentFacingLeft;
	}

	void Update()
	{
		// Facing
		float valueCheck = _motor.normalizedXMovement;

		if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
			_motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
			_motor.motorState == PlatformerMotor2D.MotorState.Jumping)
		{
			valueCheck = _motor.velocity.x;
		}

		if (Mathf.Abs(valueCheck) >= 0.1f)
		{
			Vector3 newScale = visualChild.transform.localScale;
			newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
			visualChild.transform.localScale = newScale;
		}
	}

	private void SetCurrentFacingLeft()
	{
		_currentFacingLeft = _motor.facingLeft;
	}
}
