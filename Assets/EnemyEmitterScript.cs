using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class EnemyEmitterScript : MonoBehaviour {
	public Wave[] waves;
	private List<GameObject> _emitted = new List<GameObject>() ;// _emitted = new List<GameObject>();

	void Start () {
		
	}

	void Spawn () {
		
	}

	// Update is called once per frame
	void Update () {
		foreach (Wave currentWave  in this.waves) {
			if (currentWave.is_complete == true) {
				continue;
			}
			bool is_wave_complete = true;
			foreach (EnemyContainter Enemy in currentWave.enemy_containers) {
				if (!Enemy.isComplete()) {
					is_wave_complete = false;
				}
				if (Enemy.isReady () == true) {
					GameObject ins = (GameObject)Instantiate ( Enemy.GetEnemy () /* Enemy.EnemyType */);
					Debug.Log (ins.gameObject);
					if (ins.gameObject == null) {
						
					}

					this._emitted.Add (ins);
					if (Enemy.StartPos != null) {
						ins.transform.position = Enemy.StartPos.transform.position;
					}
				}
			}
			if (is_wave_complete) {
				//parameterList.RemoveAll(item => item == null);
				this._emitted.RemoveAll(item => item == null);
				if (this._emitted.Count == 0) {
					currentWave.is_complete = true;
				
					foreach (string spell_name in currentWave.spells_rewards) {
						DrawProcessor._instance.activateSpell (spell_name);
						Debug.Log ("Spell Given");
					}
					Debug.Log ("Wave Complete");
					SceneControlScript.CompleteLevel ();
				}
			}
		}
	}
}
