using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public  class GameControl : MonoBehaviour {
	public Dictionary<string,int> game_prefs;
	private static string data_path = "";

	public static void SaveData<T>(string filename, T input) {
		string path = data_path+filename;
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Create (path);
		List<String> spells_data = new List<String>();
	
		bf.Serialize (fs, spells_data);
		fs.Close ();
	}

	public static T LoadData<T>(string filename ) {
			string path = data_path+filename;
			if (!File.Exists (path)) {
			return default(T);
			}
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open (path,FileMode.Open);
			T _data = (T)bf.Deserialize (fs);
			fs.Close ();
			return _data; 
	}

	// Use this for initialization
	public 	void Start () {
		data_path = Application.persistentDataPath + "/";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
