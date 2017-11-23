using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: colocar uns eventos para quando o personagem virar para atualizar as anims
public class AnimationController : MonoBehaviour
{

	public GameObject visualChild;

	protected PlatformerMotor2D _motor;
	protected Animator _animator;
	//protected bool _isJumping;
	protected bool _currentFacingLeft;

	public Animator Animator
	{
		get
		{
			return _animator;
		}
	}

	protected virtual bool TurnCondictions
	{
		get
		{
			return _motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
			_motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
			_motor.motorState == PlatformerMotor2D.MotorState.Jumping;
		}
	}

	protected virtual void Start()
	{
		_motor = GetComponent<PlatformerMotor2D>();

		_animator = visualChild.GetComponent<Animator>();
		//_animator.Play("Idle");

		_motor.onJump += SetCurrentFacingLeft;
	}

	protected virtual void Update()
	{
		// Facing
		//float valueCheck = _motor.normalizedXMovement;

		//if (TurnCondictions)
		//{
		//	valueCheck = _motor.velocity.x;
		//}

		//if (Mathf.Abs(valueCheck) >= 0.1f)
		//{
		//	//Vector3 newScale = visualChild.transform.localScale;
		//	//newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
		//	//visualChild.transform.localScale = newScale;
		//	SetCurrentFacing((valueCheck > 0) ? 1 : -1);
		//}

		//Vector3 newScale2 = visualChild.transform.localScale;
		//newScale2.x = Mathf.Abs(newScale2.x) * (_motor.facingLeft ? -1.0f : 1.0f);
		//visualChild.transform.localScale = newScale2;
	}

	private void LateUpdate()
	{
		float valueCheck = _motor.normalizedXMovement;

		if (TurnCondictions)
		{
			valueCheck = _motor.velocity.x;
		}

		if (Mathf.Abs(valueCheck) >= 0.1f)
		{
			SetCurrentFacing((valueCheck > 0) ? 1 : -1);
		}
	}


	protected void SetCurrentFacingLeft()
	{
		_currentFacingLeft = _motor.facingLeft;
	}

	public void SetCurrentFacing(int faceDir)
	{
		float sign = Mathf.Sign(faceDir);

		Vector3 newScale = visualChild.transform.localScale;
		newScale.x = Mathf.Abs(newScale.x) * sign;
		visualChild.transform.localScale = newScale;
		SetCurrentFacingLeft();
		_motor.facingLeft = sign == -1 ? true : false;
	}

	public void SetCurrentAnimation(string anim)
	{
		_animator.Play(anim);
	}
}
