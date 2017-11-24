using UnityEngine;
using Seven.Stats;

public class StatsController<T> : StatsController where T : Stat
{
	[SerializeField]
	protected T health;

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

		if (health.Value <= 0)
			Death();
		else
			if (OnTakeDamage != null)
				OnTakeDamage(dmg);
	}

	protected virtual void Death()
	{
		Dead = true;

		if (OnDeath != null)
			OnDeath();
	}
}

public class StatsController : MonoBehaviour
{
	public bool Dead { get; protected set; }

	public System.Action<int> OnTakeDamage;
	public System.Action OnDeath;
}
