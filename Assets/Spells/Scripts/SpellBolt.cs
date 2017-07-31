using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellBolt : BaseSpell {
	public int cost  = 2;
	private float  _speed = 5f;
	//Rigidbody2D rigidbody2D;

	void Start () {
		//rigidbody2D = GetComponent<Rigidbody2D>();		
		Cast ();
	}


	public void Cast () {
		//new Vec
		Debug.Log(this._speed);

		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (this._speed, 0);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (this._health);
	}
}