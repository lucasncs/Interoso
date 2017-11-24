using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllDeadConditions : MonoBehaviour
{
	public StatsController[] npcs;
	public UnityEvent OnDeath;
	
	void Start()
	{

	}
	
	void Update()
	{
		int alreadyDead = 0;

		foreach (var npc in npcs)
		{
			if (npc.Dead)
			{
				alreadyDead++;
			}
		}

		if (alreadyDead == npcs.Length)
			OnDeath.Invoke();
	}
}
