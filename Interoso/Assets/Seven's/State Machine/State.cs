namespace Seven.StateMachine
{
	public abstract class State<T> : State where T : StateMachine<T>
	{
		protected T machine;

		public State(T machine)
		{
			this.machine = machine;
		}
	}

	public abstract class State
	{
		public abstract void Tick();

		public virtual void OnStateEnter() { }
		public virtual void OnStateExit() { }
	}
}