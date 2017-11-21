using UnityEngine;
using Seven.Stats;

public class StatsController<T> : MonoBehaviour where T : Stat
{
	[SerializeField]
	protected T health;

	public System.Action OnTakeDamage;
	public System.Action OnDeath;

	public T Health
	{
		get
		{
			return health;
		}
	}

	protected virtual void Awake()
	{
		health.Initialize();
	}

	public virtual void Damage(int dmg)
	{
		health.Value -= dmg;

		if (OnTakeDamage != null)
			OnTakeDamage();

		if (health.Value <= 0)
			Death();
	}

	protected virtual void Death()
	{
		if (OnDeath != null)
			OnDeath();
	}
}
