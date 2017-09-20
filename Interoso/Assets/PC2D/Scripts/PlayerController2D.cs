using System.Collections;
using UnityEngine;

/// <summary>
/// This class is a simple example of how to build a controller that interacts with PlatformerMotor2D.
/// </summary>
[RequireComponent(typeof(PlatformerMotor2D))]
public class PlayerController2D : MonoBehaviour
{
	private PlatformerMotor2D _motor;
	private bool _restored = true;
	private bool _enableOneWayPlatforms;
	private bool _oneWayPlatformsAreWalls;

	private Rigidbody2D _body2D;
	private HookController _hook;

	private Shooter _shot;
	

	void Start()
	{
		_motor = GetComponent<PlatformerMotor2D>();
		_body2D = GetComponent<Rigidbody2D>();
		_hook = GetComponent<HookController>();
		_shot = GetComponent<Shooter>();
	}

	void Update()
	{
		// use last state to restore some ladder specific values
		if (_motor.motorState != PlatformerMotor2D.MotorState.FreedomState)
		{
			// try to restore, sometimes states are a bit messy because change too much in one frame
			FreedomStateRestore(_motor);
		}

		#region Jump
		// If you want to jump in ladders, leave it here, otherwise move it down
		if (Input.GetButtonDown(PC2D.Input.JUMP))
		{
			Jump();
		}

		_motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);
		#endregion

		#region Moviment
		// XY freedom movement
		if (_motor.motorState == PlatformerMotor2D.MotorState.FreedomState)
		{
			_motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
			_motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);

			return; // do nothing more
		}

		// X axis movement
		if (Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > PC2D.Globals.INPUT_THRESHOLD)
		{
			_motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
		}
		else
		{
			_motor.normalizedXMovement = 0;
		}

		if (Input.GetAxis(PC2D.Input.VERTICAL) != 0)
		{
			bool up_pressed = Input.GetAxis(PC2D.Input.VERTICAL) > 0;
			if (_motor.IsOnLadder())
			{
				if (
					(up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Top)
					||
					(!up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Bottom)
				 )
				{
					// do nothing!
				}
				// if player hit up, while on the top do not enter in freeMode or a nasty short jump occurs
				else
				{
					// example ladder behaviour

					_motor.FreedomStateEnter(); // enter freedomState to disable gravity
					_motor.EnableRestrictedArea();  // movements is retricted to a specific sprite bounds

					// now disable OWP completely in a "trasactional way"
					FreedomStateSave(_motor);
					_motor.enableOneWayPlatforms = false;
					_motor.oneWayPlatformsAreWalls = false;

					// start XY movement
					_motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
					_motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);
				}
			}
		}
		else if (Input.GetAxis(PC2D.Input.VERTICAL) < -PC2D.Globals.FAST_FALL_THRESHOLD)
		{
			_motor.fallFast = false;
		}
		#endregion

		#region Hook
		if (Input.GetButtonDown(PC2D.Input.HOOK) && !_hook.isHooked)
		{
			_hook.UseHook();
			_motor.enabled = !_hook.isHooked;
			_body2D.velocity = Vector2.zero;
		}

		if (_hook.isHooked)
		{
			float dir = Input.GetAxis(PC2D.Input.HORIZONTAL);
			if (Input.GetButton/*Down*/(PC2D.Input.HORIZONTAL))
			{
				_body2D.AddForce(Vector2.right * Mathf.Sign(dir) * 2);
				//_body2D.velocity += Vector2.right * Mathf.Sign(dir) * 2;
				//var v = new Vector2(
				//	Mathf.Abs(_body2D.velocity.x),
				//	Mathf.Abs(_body2D.velocity.y)
				//	);
				//_body2D.velocity += v * Mathf.Sign(dir);
				_body2D.velocity = Vector2.ClampMagnitude(_body2D.velocity, 9);
			}
			if (Input.GetButtonDown(PC2D.Input.JUMP))
			{
				//_body2D.velocity += Vector2.right * Mathf.Sign(dir);
				//_body2D.AddForce(Vector2.right * Mathf.Sign(dir) * 20);
				StartCoroutine(StopVelocity());
				_motor.enabled = true;
				_hook.Stop();
			}
		}
		#endregion

		if (Input.GetButtonDown(PC2D.Input.SHOOT))
		{
			_shot.Shoot(_motor.facingLeft ? -1 : 1);
		}
	}

	private void Jump()
	{
		_motor.Jump();
		_motor.DisableRestrictedArea();
	}

	IEnumerator StopVelocity()
	{
		while (_motor.collidingAgainst == PlatformerMotor2D.CollidedSurface.None)
		{
			//_body2D.velocity = Vector2.down * 2;
			_body2D.velocity = new Vector2(Mathf.Clamp(_body2D.velocity.x, -5, 5), -2);
			yield return null;
		}
		_body2D.velocity = Vector2.zero;
	}

	// before enter en freedom state for ladders
	void FreedomStateSave(PlatformerMotor2D motor)
	{
		if (!_restored) // do not enter twice
			return;

		_restored = false;
		_enableOneWayPlatforms = _motor.enableOneWayPlatforms;
		_oneWayPlatformsAreWalls = _motor.oneWayPlatformsAreWalls;
	}
	// after leave freedom state for ladders
	void FreedomStateRestore(PlatformerMotor2D motor)
	{
		if (_restored) // do not enter twice
			return;

		_restored = true;
		_motor.enableOneWayPlatforms = _enableOneWayPlatforms;
		_motor.oneWayPlatformsAreWalls = _oneWayPlatformsAreWalls;
	}
}
