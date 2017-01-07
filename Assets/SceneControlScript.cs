using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControlScript : MonoBehaviour {
	public static SceneControlScript instance = null; 
	public static int last_available_scene_id = 1;
	public static int state = 0;
	public static int STATE_MENU = 0;
	public static int STATE_GAME = 1;
	public static List<string> scenes = new List<string> () {
		"MainScene",
		"Wave2Scene"
	};

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);
	}


	public static void LoadGameLevel(string scene_name) {
		Application.LoadLevel (scene_name);
		state = STATE_GAME;
	}

	public static void LoadMenu() {
		state = STATE_MENU;
		Application.LoadLevel ("LevelsList");
	}

	public static void  FailLevel() {
		LoadMenu();
	}

	public static void CompleteLevel() {
		if (state == STATE_GAME) {
			last_available_scene_id++;
		}
		LoadMenu();
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnGUI () {
		int top =20;
		int top_offset = 40;

		if (state == STATE_GAME) {
			if (GUI.Button (new Rect (10, top, 100, 30), "[X]")) {
				LoadMenu ();
			}
		}

		if (state == STATE_MENU) {
			
			for (int i = 0; i <= last_available_scene_id && i < scenes.Count; i++) {//   (string scene_name as this.scenes) {
				string scene_name = scenes [i];
				top += top_offset;
				if (GUI.Button (new Rect (10, top, 100, 30), "" + scene_name)) {
					LoadGameLevel (scene_name);
				}
			}
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
