using UnityEngine;
using Seven.StateMachine;

public class EnemyStateMachine : StateMachine<EnemyStateMachine>
{
	private PlatformerMotor2D _motor;

	public float movement { get; private set; }


	public Vector2[] patrolWaypoints;

	protected override void Start()
	{
		_motor = GetComponent<PlatformerMotor2D>();
		movement = -1;

		for (int i = 0; i < patrolWaypoints.Length; i++)
		{
			patrolWaypoints[i] = patrolWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
        }

		SetState(new PatrolState(this));
	}

	public void Move(Vector2 direction, float velocityMultiplier)
	{
		_motor.normalizedXMovement = velocityMultiplier * Direction(direction);
	}

	private int Direction(Vector2 dir)
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



	void OnDrawGizmos()
	{
		if (patrolWaypoints != null)
		{
			Gizmos.color = Color.red;
			float size = .3f;

			for (int i = 0; i < patrolWaypoints.Length; i++)
			{
				Vector2 globalWaypointPos = /*(Application.isPlaying) ? */patrolWaypoints[i] /*: patrolWaypoints[i] + new Vector2(transform.position.x, transform.position.y)*/;
				Gizmos.DrawLine(globalWaypointPos - Vector2.up * size, globalWaypointPos + Vector2.up * size);
				Gizmos.DrawLine(globalWaypointPos - Vector2.left * size, globalWaypointPos + Vector2.left * size);
			}
		}
	}

	void Reset()
	{
	}
}
