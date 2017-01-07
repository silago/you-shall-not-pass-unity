using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : GameElement {
	
	public float _speed = 200.0f;
	// Use this for initialization
	void Start () {
		//this.transform.position+= new Vector3 (0, 0, this.transform.position.z);
		//this.transform.position = new Vector3 (
		//	3,
		//	0
		//);
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (-this._speed, 0);

	}


	void OnGUI(){
		//return;
		var point = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
		var x = point.x;
		var y = Screen.height - point.y+20; // bottom left corner set to the 3D point
		GUI.Label(new Rect(x,y,500,100),this._health.ToString()); // display its name, or other string
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log ("QE");

		//this.transform.position = new Vector3 (
		//	this.transform.position.x- Time.deltaTime*this._speed,
		//	this.transform.position.y,
		//	this.transform.position.z
		//);
	}
}
