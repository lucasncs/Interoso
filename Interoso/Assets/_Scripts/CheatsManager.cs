using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
	private PlayerStats player;

	public bool playerImortal;

	private void Awake()
	{
		player = FindObjectOfType<PlayerStats>();
	}

	private void Update()
	{
		if (playerImortal) PlayerLifeUp();
	}

	private void PlayerLifeUp()
	{
		player.Health.Value = player.Health.MaxVal;
	}
}
