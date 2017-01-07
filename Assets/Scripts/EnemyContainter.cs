using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyContainter  {
	public int count = 1;
	public float interval = 1000;
	public float delay = 0; 
	public GameObject EnemyType;
	public GameObject StartPos;
	float timer  = 0;
	//private List<GameObject> _emitted;


	public GameObject GetEnemy() {
		this.count--;
		return this.EnemyType;
	}

	public bool isComplete() {
		return (this.count <= 0);
	}

	public bool isReady () {
		if (this.isComplete() || this.delay > 0) {
			this.delay -= Time.deltaTime;
			return false;
		}

		this.timer -= Time.deltaTime;
		if (this.timer <= 0) {
			this.timer = this.interval;
			return true;
		} else
			return false;
	}
}
