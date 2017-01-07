using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public in

public class WizardScript : GameElement {

	public float _speed = 200.0f;
	// Use this for initialization
	void Start () {
	}
	void Die() {
		base.Die ();
		SceneControlScript.FailLevel ();

	}
}

