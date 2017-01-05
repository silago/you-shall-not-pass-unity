using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellLaser : BaseSpell {
	public int  cost  = 2;
	public  int _speed = 300;
	private float _hit_interval = 3;
	private float _hit_interval_timer = 0.1f;

	Dictionary<int,Collider2D> colliders = new Dictionary<int,Collider2D>() ;

	void Start () {
		Cast ();
	}

	public void Cast () {
		//this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Time.deltaTime*this._speed, 0);
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (this.colliders.ContainsKey (coll.GetInstanceID ())) {
			this.colliders.Remove (coll.GetInstanceID ());
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Hit (coll);
		if (this.colliders ==	 null) {
			return;
		}
		if (!this.colliders.ContainsKey (coll.GetInstanceID ())) {
			this.colliders.Add (coll.GetInstanceID (), coll);
		}

	}

	void Hit(Collider2D coll) {
			coll.SendMessage("GetHit", this._damage);
	}

	void FixedUpdate () {
		_hit_interval_timer -= Time.deltaTime;
		if (_hit_interval_timer <= 0) {
			_hit_interval_timer = _hit_interval;

			foreach (KeyValuePair<int,Collider2D> kv in this.colliders) {
				if (kv.Value == null)
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