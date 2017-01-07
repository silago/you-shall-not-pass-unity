using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCloud : BaseSpell {

	void Start () {
		Cast ();
	}


	public void Cast () {
		this.gameObject.transform.position = SceneControlScript.UPPER_LINE;
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

	void Update () {
		this._health -= Time.deltaTime;
		if (this._health < 0) {
			this.Die();
		}
	}
}
