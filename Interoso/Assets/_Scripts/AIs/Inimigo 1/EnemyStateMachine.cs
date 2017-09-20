using System;
using UnityEngine;
using Seven.StateMachine;

public class EnemyStateMachine : StateMachine<EnemyStateMachine>
{
	private PlatformerMotor2D _motor;
	private AnimationController _visual;
	private Shooter _shot;
	
	public float distForwDetection;
	public float distBackDetection;
	public LayerMask detectionLayer;
	public float fireRate = .5f;

	[HideInInspector]
	public Transform player;

	public Vector2[] patrolWaypoints;

	public int faceDirection
	{
		get
		{
			return _motor.facingLeft ? -1 : 1;
        }

		private set
		{
			_motor.facingLeft = Mathf.Sign(value) == -1 ? true : false;
		}
	}


	protected override void Start()
	{
		_motor = GetComponent<PlatformerMotor2D>();
		_visual = GetComponent<AnimationController>();
		_shot = GetComponent<Shooter>();

		for (int i = 0; i < patrolWaypoints.Length; i++)
		{
			patrolWaypoints[i] = patrolWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
		}

		SetState(new PatrolState(this));
	}

	public void Move(Vector2 direction, float velocityMultiplier)
	{
		faceDirection = DirectionToGo(direction);
        _motor.normalizedXMovement = velocityMultiplier * faceDirection;
    }

	public void StopMoving()
	{
		_motor.normalizedXMovement = 0;
    }

	private int DirectionToGo(Vector2 dir)
	{
		Vector2 relativePoint = transform.InverseTransformPoint(dir);
		if (relativePoint.x < 0.0) //Point is to the left
			return -1;

		return 1;
	}

	public Vector2 NextWaypoint(int index)
	{
		return patrolWaypoints[index];
	}

	public bool InRangeForDetection()
	{
		Vector2 dir = _motor.facingLeft ? Vector2.left : Vector2.right;
        if (ThrowDetectionRaycast(dir, distForwDetection) || ThrowDetectionRaycast(-dir, distBackDetection))
		{
			return true;
		}
		return false;
	}

	private bool ThrowDetectionRaycast(Vector2 dir, float distance)
	{
		RaycastHit2D hit = Physics2D.Raycast(
			transform.position,
			dir,
			distance,
			detectionLayer);

		if (hit.collider != null)
		{
			player = hit.transform;
			return true;
		}
		return false;
	}

	public void LookToPlayer()
	{
		if (player)
		{
			int i = DirectionToGo(player.transform.position);
			//faceDirection = i;
			_visual.SetCurrentFacing(i);
		}
	}

	public void Shoot()
	{
		_shot.Shoot(faceDirection);
	}




	void OnDrawGizmos()
	{
		if (patrolWaypoints != null)
		{
			Gizmos.color = Color.blue;
			float size = .3f;

			for (int i = 0; i < patrolWaypoints.Length; i++)
			{
				Vector2 globalWaypointPos = (Application.isPlaying) ? patrolWaypoints[i] : patrolWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
				Gizmos.DrawLine(globalWaypointPos - Vector2.up * size, globalWaypointPos + Vector2.up * size);
				Gizmos.DrawLine(globalWaypointPos - Vector2.left * size, globalWaypointPos + Vector2.left * size);
			}
		}

		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;
			System.Func<float, float, Vector2> to = (dist, dir) => new Vector2(dist * dir + transform.position.x, transform.position.y);
			Gizmos.DrawLine(transform.position, to(distForwDetection, faceDirection));
			Gizmos.DrawLine(transform.position, to(distBackDetection, -faceDirection));
		}
	}

	void Reset()
	{
	}
}
