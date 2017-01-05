using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellGolem : BaseSpell {
	public int  cost  = 2;
	public  int _speed = 300;
	private float _hit_interval = 3;
	private float _hit_interval_timer = 0.1f;

	Dictionary<int,Collision2D> colliders = new Dictionary<int,Collision2D>() ;

	void Start () {
		Cast ();
	}

	public void Cast () {
		//this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Time.deltaTime*this._speed, 0);
	}
		
	void OnCollisionExit2D(Collision2D coll) {
		if (this.colliders.ContainsKey (coll.gameObject.GetInstanceID ())) {
			this.colliders.Remove (coll.gameObject.GetInstanceID ());
		}
	}
	//void OnCollisionEnter2D(Collision2D coll) {
		
	void OnCollisionEnter2D(Collision2D coll) {
		Hit (coll);
		if (this.colliders ==	 null) {
			return;
		}
		if (!this.colliders.ContainsKey (coll.gameObject.GetInstanceID ())) {
			this.colliders.Add (coll.gameObject.GetInstanceID (), coll);
		}

	}

	void Hit(Collision2D coll) {
		coll.gameObject.SendMessage("GetHit", this._damage);
	}

	void FixedUpdate () {
		_hit_interval_timer -= Time.deltaTime;
		if (_hit_interval_timer <= 0) {
			_hit_interval_timer = _hit_interval;

			foreach (KeyValuePair<int,Collision2D> kv in this.colliders) {
				if (kv.Value.gameObject == null)
					continue;
				Hit(kv.Value);
			}
		}
		this._health -= Time.deltaTime;
		if (this._health < 0) {
			this.Die();
		}
	}
}