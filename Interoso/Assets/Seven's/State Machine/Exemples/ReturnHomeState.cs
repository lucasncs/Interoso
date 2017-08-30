//using UnityEngine;
//using Seven.StateMachine;

//namespace Seven.Examples.StateMachines
//{
//	public class ReturnHomeState : State
//	{
//		private Vector3 destination;

//		public ReturnHomeState(StateMachine.StateMachine machine) : base(machine)
//		{
//		}

//		public override void Tick()
//		{
//			//machine.MoveToward(destination);

//			if (ReachedHome())
//			{
//				machine.SetState(new WanderState(machine));
//			}
//		}

//		public override void OnStateEnter()
//		{
//			machine.GetComponent<Renderer>().material.color = Color.blue;
//		}

//		private bool ReachedHome()
//		{
//			return Vector3.Distance(machine.transform.position, destination) < 0.5f;
//		}
//	}
//}