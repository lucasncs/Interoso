using System;
using UnityEngine;
using Seven.StateMachine;
using System.Collections;

public class EnemyStateMachine : StateMachine<EnemyStateMachine>
{
	[SerializeField]
	protected PlatformerMotor2D _motor;
	protected EnemyAnimationController _visual;
	protected EnemyStats _stats;

	public float distForwDetection;
	public float distBackDetection;
	public LayerMask detectionLayer;
	public float fireRate = .5f;

	public State PatrolState;
	public State AttackState;

	[HideInInspector]
	public Transform player;

	public float walkSpeed = .3f;

	public Vector2[] patrolWaypoints;

	public EnemyAnimationController Animation
	{
		get
		{
			return _visual;
		}
	}

	public int FaceDirection
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
		_visual = GetComponent<EnemyAnimationController>();
		_stats = GetComponent<EnemyStats>();

		_stats.OnDeath += OnDeath;
		_stats.OnTakeDamage += OnTakeDamage;

		for (int i = 0; i < patrolWaypoints.Length; i++)
		{
			patrolWaypoints[i] = patrolWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
		}

		SetState(PatrolState);
	}

	protected override void Update()
	{
		if (!_visual.Damage)
			base.Update();
	}

	public void Move(Vector2 direction, float velocityMultiplier)
	{
		FaceDirection = DirectionToGo(direction);
		_motor.normalizedXMovement = velocityMultiplier * FaceDirection;
	}

	public void StopMoving()
	{
		_motor.normalizedXMovement = 0;
	}

	protected int DirectionToGo(Vector2 dir)
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

	public virtual bool InRangeToAttack()
	{
		return InRangeForDetection();
	}

	protected bool ThrowDetectionRaycast(Vector2 dir, float distance)
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
			FaceDirection = i;
			_visual.SetCurrentFacing(i);
		}
	}

	public virtual void Attack(bool attk = true)
	{
		// Control attck anim
		_visual.Attack = attk;
	}


	private void OnTakeDamage()
	{
		_visual.Damage = true;
		this.Invoke(() => _visual.Damage = false, _visual.damageDuration);
	}

	private void OnDeath()
	{
		StartCoroutine(DeathFade());
		enabled = false;
		_visual.Animator.enabled = false;
	}
	private IEnumerator DeathFade()
	{
		SpriteRenderer rendr = _visual.visualChild.GetComponent<SpriteRenderer>();
		Color c = rendr.color;

		while (c.a > 0)
		{
			c.a -= Time.deltaTime * 5;
			rendr.color = c;
			yield return null;
		}

		gameObject.SetActive(false);
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
			Gizmos.DrawLine(transform.position, to(distForwDetection, FaceDirection));
			Gizmos.DrawLine(transform.position, to(distBackDetection, -FaceDirection));
		}
	}

	protected virtual void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		System.Func<float, float, Vector2> to = (dist, dir) => new Vector2(dist * dir + transform.position.x, transform.position.y);
		Gizmos.DrawLine(transform.position, to(distForwDetection, FaceDirection));
		Gizmos.DrawLine(transform.position, to(distBackDetection, -FaceDirection));
	}

	void Reset()
	{
	}
}
