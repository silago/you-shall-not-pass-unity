using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public in

public class WizardScript : GameElement {
	public float _mana  = 100.0f;
	//private float _mana_regain;
	public float _speed = 200.0f;
	public const string TAG = "Player";

	void OnGUI(){
		//return;
		var point = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
		var x = point.x;
		var y = Screen.height - point.y+20; // bottom left corner set to the 3D point
		GUI.Label(new Rect(x,y,500,100),this._health.ToString()); // display its name, or other string
	}

	public override void Die() {
		SceneControlScript.FailLevel ();
		Debug.Log ("YOU DEAD");
		base.Die ();

	}
}

