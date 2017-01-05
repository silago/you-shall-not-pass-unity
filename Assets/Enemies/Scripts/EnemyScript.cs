using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : GameElement {
	
	public float _speed = 0.1f;
	// Use this for initialization
	void Start () {
		//this.transform.position+= new Vector3 (0, 0, this.transform.position.z);
		//this.transform.position = new Vector3 (
		//	3,
		//	0
		//);
	}



	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (
			this.transform.position.x- Time.deltaTime*this._speed,
			this.transform.position.y,
			this.transform.position.z
		);
	}
}
