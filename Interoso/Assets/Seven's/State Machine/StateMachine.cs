using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seven.StateMachine
{
	public class StateMachine : MonoBehaviour
	{
		private State currentState;

		protected virtual void Start()
		{
			//SetState(new GenericState(this));
		}

		private void Update()
		{
			currentState.Tick();
		}

		public void SetState(State state)
		{
			if (currentState != null)
				currentState.OnStateExit();

			currentState = state;

			if (currentState != null)
				currentState.OnStateEnter();
		}
	}
}