using UnityEngine;

namespace Seven.StateMachine
{
	public abstract class StateMachine<T> : StateMachine where T : StateMachine<T>
	{
		protected virtual void Start()
		{
			//SetState(new GenericState(this));
		}

		protected virtual void Update()
		{
			currentState.Tick();
		}

		public override void SetState(State state)
		{
			if (currentState != null)
				currentState.OnStateExit();

			currentState = state;

			if (currentState != null)
				currentState.OnStateEnter();
		}
	}

	public abstract class StateMachine : MonoBehaviour
	{
		protected State currentState;

		public abstract void SetState(State state);
    }
}