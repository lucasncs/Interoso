namespace Seven.StateMachine
{
	public abstract class State
	{
		//protected StateMachine machine;

		public abstract void Tick();

		public virtual void OnStateEnter() { }
		public virtual void OnStateExit() { }

		//public State(StateMachine machine)
		//{
		//	this.machine = machine;
		//}
	}
}