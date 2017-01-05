using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class GameElement : MonoBehaviour {

	public float	_health	= 100	;
	public float	_damage	= 50	;
	public bool	_destroyable = true	;


	public void Die() {
		Destroy (this.gameObject);
	}

	/*
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("col");
		if (coll.gameObject.tag != this.gameObject.tag) {
			coll.gameObject.SendMessage("GetHit", this._damage);
		}
	}
	*/

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log ("trig");
		if (coll.gameObject.tag != this.gameObject.tag) {
			coll.gameObject.SendMessage("GetHit", this._damage);
		}
	}

	public void GetHit(int damage) {
		//Debug.Log (_health);
		if (this._destroyable) {
			this._health -= damage;
		}
		if (this._health <= 0) {
			this.Die ();
		}
		//Debug.Log (this._health);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
