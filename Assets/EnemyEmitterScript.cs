using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class EnemyEmitterScript : MonoBehaviour {
	public Wave[] waves;
	void Start () {
		
	}

	void Spawn () {
		
	}

	// Update is called once per frame
	void Update () {
		//return;
		//this.timer+=Time.deltaTime;
		foreach (Wave currentWave  in this.waves) {
			//if (currentWave.
			if (currentWave.is_complete == true) {
				continue;
			}
			bool is_wave_complete = true;
			foreach (EnemyContainter Enemy in currentWave.enemy_containers) {
				if (!Enemy.isComplete()) {
					is_wave_complete = false;
				}
				if (Enemy.isReady () == true) {
					GameObject ins = Instantiate ( Enemy.GetEnemy () /* Enemy.EnemyType */);
					Debug.Log ("Emitted");

					if (Enemy.StartPos != null) {
						ins.transform.position = Enemy.StartPos.transform.position;
						Debug.Log ("Position Set");
					}
				}
			}
			if (is_wave_complete) {
				currentWave.is_complete = true;
				foreach (string spell_name in currentWave.spells_rewards) {
					DrawProcessor._instance.activateSpell (spell_name);
					Debug.Log ("Spell Given");
				}
				Debug.Log ("Wave Complete");

			}
		}
	}
}
