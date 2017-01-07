using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsListScript : MonoBehaviour {
	void Start () {
		
	}

	void OnGUI () {
		if (GUI.Button (new Rect (10, 60, 100, 30), "Load Scene 1")) {
			Application.LoadLevel ("MainScene");
		}

		if (GUI.Button (new Rect (10, 100, 100, 30), "Load Scene 2")) {
			//Application.LoadLevel (2);
		}
	}

	void Update () {
		
	}
}
