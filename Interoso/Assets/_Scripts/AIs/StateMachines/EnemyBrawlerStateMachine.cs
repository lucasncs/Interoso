using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrawlerStateMachine : EnemyStateMachine
{
	private Shooter _shot;

	public float meleeRange = 3;

	private Coroutine attck;

	private void Awake()
	{
		PatrolState = new PatrolState(this);
		AttackState = new MeleeState(this);

		_shot = GetComponent<Shooter>();
	}

	public override bool InRangeToAttack()
	{
		Vector2 dir = _motor.facingLeft ? Vector2.left : Vector2.right;
		if (ThrowDetectionRaycast(dir, meleeRange)/* || ThrowDetectionRaycast(-dir, distBackDetection)*/)
		{
			return true;
		}
		return false;
	}

	public override void Attack()
	{
		//if (attck != null) StopCoroutine(attck);

		//meleeCollider.enabled = true;

		//attck = this.Invoke(() => meleeCollider.enabled = false, fireRate * .5f);
		_shot.Shoot(FaceDirection);
	}


	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();

		Gizmos.color = Color.cyan;
		System.Func<float, float, Vector2> to = (dist, dir) => new Vector2(dist * dir + transform.position.x, transform.position.y);
		Gizmos.DrawLine(transform.position, to(meleeRange, FaceDirection));
		//Gizmos.DrawLine(transform.position, to(distBackDetection, -FaceDirection));
	}
}
