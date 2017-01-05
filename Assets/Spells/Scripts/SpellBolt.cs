using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellBolt : BaseSpell {
	public int cost  = 2;
	public int _speed = 300;
	//Rigidbody2D rigidbody2D;

	void Start () {
		//rigidbody2D = GetComponent<Rigidbody2D>();		
		Cast ();
	}


	public void Cast () {
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Time.deltaTime*this._speed, 0);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (this._health);
	}
}