//using UnityEngine;
//using Seven.StateMachine;

//namespace Seven.Examples.StateMachines
//{
//	public class WanderState : State
//	{
//		private Vector3 nextDestination;

//		private float wanderTime = 5f;

//		private float timer;

//		public WanderState(StateMachine.StateMachine machine) : base(machine)
//		{
//		}

//		public override void OnStateEnter()
//		{
//			nextDestination = GetRandomDestination();
//			timer = 0f;
//			machine.GetComponent<Renderer>().material.color = Color.green;
//		}

//		private Vector3 GetRandomDestination()
//		{
//			return new Vector3(
//				UnityEngine.Random.Range(-40, 40),
//				0f,
//				UnityEngine.Random.Range(-40, 40)
//				);
//		}

//		public override void Tick()
//		{
//			if (ReachedDestination())
//			{
//				nextDestination = GetRandomDestination();
//			}

//			//machine.MoveToward(nextDestination);

//			timer += Time.deltaTime;
//			if (timer >= wanderTime)
//			{
//				machine.SetState(new ReturnHomeState(machine));
//			}
//		}

//		private bool ReachedDestination()
//		{
//			return Vector3.Distance(machine.transform.position, nextDestination) < 0.5f;
//		}
//	}
//}