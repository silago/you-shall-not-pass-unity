using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellGolem : BaseSpell {
	public int  cost  = 2;
	public  int _speed = 300;


	Dictionary<int,Collision2D> colliders = new Dictionary<int,Collision2D>() ;

	void Start () {
		this._hit_interval = 3;
		this._hit_interval_timer = 0.1f;
		Cast ();
	}

	public void Cast () {
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position+(new Vector3(1,0)), Vector2.right);
		if (hit.collider != null) {
			Debug.Log (hit.collider.gameObject.name);
			var old_pos = this.transform.position;
			this.transform.position = new Vector3 (
				hit.collider.gameObject.transform.position.x-1,
				old_pos.y,
				old_pos.z
			);
		}
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