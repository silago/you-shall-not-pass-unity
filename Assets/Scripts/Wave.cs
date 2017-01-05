using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave  {
	public EnemyContainter[] enemy_containers;
	public List<string> spells_rewards;
	public bool is_complete = false;
}