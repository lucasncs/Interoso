using UnityEngine;
using Seven.StateMachine;

public class PatrolState : State<EnemyStateMachine>
{
	private int waypointIndex;
	private Vector2 nextDestination;

	public PatrolState(EnemyStateMachine machine) : base(machine)
	{
	}

	public override void OnStateEnter()
	{
		machine.GetComponentInChildren<SpriteRenderer>().color = Color.green;
		nextDestination = machine.patrolWaypoints[waypointIndex];
	}

	public override void Tick()
	{
		if (ReachedDestination())
		{
			nextDestination = machine.NextWaypoint(NextIndex());
		}
		else
		{
			machine.Move(nextDestination, 0.3f);
		}
	}

	int NextIndex()
	{
		waypointIndex++;
		if (waypointIndex >= machine.patrolWaypoints.Length) waypointIndex = 0;
		return waypointIndex;
	}

	private bool ReachedDestination()
	{
		return Vector2.Distance(machine.transform.position, nextDestination) < 0.5f;
	}
}
