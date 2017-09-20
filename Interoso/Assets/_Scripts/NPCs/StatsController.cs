using UnityEngine;
using Seven.Stats;

public class StatsController : MonoBehaviour
{
	[SerializeField]
	private Stat health;

	public System.Action OnTakeDamage;
	public System.Action OnDeath;

	public Stat Health
	{
		get
		{
			return health;
		}
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
