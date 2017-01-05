using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent(typeof(LineRenderer))]
public class DrawProcessor : MonoBehaviour {
	List<int[]> path = new List<int[]>();
	List<int[]> points = new List<int[]>();
	public List<BaseSpell> spells;
	bool active = false;
	LineRenderer lineRenderer;
	private  string active_spells_path = "";

	public static DrawProcessor _instance = null;

	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller.

	void Awake () {
		if (_instance == null) {
			_instance = this;
			this.lineRenderer = GetComponent<LineRenderer>();
			//Debug.Log (_instance);
		} else {

		}
		//return _instance;
		this.active_spells_path = Application.persistentDataPath + "/activeSpells.dat";
	}





	void loadSpells() {
		if (!File.Exists (active_spells_path)) {
			return;
		}
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (active_spells_path,FileMode.Open);

		Dictionary<String,Boolean> spells_data = (Dictionary<String,Boolean>)bf.Deserialize (fs);
		fs.Close ();

		foreach (BaseSpell spell in _instance.spells) {
			//Debug.Log (spells_data);
			if (spell == null) {
				continue;
			}
			if (spells_data.ContainsKey (spell.name)) {
				spell.active = spells_data [spell.name];
			}
		}
	}

	void saveSpells() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Create (active_spells_path);

		//Dictionary<String,Boolean> spells_data = (Dictionary<String,Boolean>)bf.Deserialize (fs);
		//fs.Close ();
		Dictionary<String,Boolean> spells_data = new Dictionary<String,Boolean>();
		foreach (BaseSpell spell in this.spells) {
			if (spell!=null && spell.name !="")
				spells_data.Add (spell.name, spell.active);
		}
		bf.Serialize (fs, spells_data);
		fs.Close ();

	}
	void activateAll() {
		foreach (BaseSpell spell in this.spells) {
			spell.active = true;
		}
	}
	void deactivateAll() {
		foreach (BaseSpell spell in this.spells) {
				spell.active = false;
		}
	}

	public void activateSpell(string spell_name) {
		foreach (BaseSpell spell in this.spells) {
			if (spell == null) {
				continue;
			}
			if (spell.spell_name == spell_name) {
				spell.active = true;
				break;
			}
		}

		this.saveSpells ();
	}

	// Use this for initialization
	void Start () {
		this.loadSpells ();
		//this.deactivateAll ();
		this.activateSpell ("Rain");
		this.activateSpell ("Bolt");
		this.activateSpell ("Laser");
		this.activateSpell ("Golem");
	}


	void Deactivate() {
		this.active = false;
		//var min_diff = 10;
		this.points.Clear();
		this.Check();
	}

	void Activate() {
		this.path.Clear ();
		this.points.Clear ();
		this.points.Add (new int[] {0,0});
		this.active = true;

	}

	void Draw() {
		if (lineRenderer == null) {
			return;
		}
		lineRenderer.numPositions = path.Count;
		for(int i = 0; i < this.path.Count; i++)
		{	
			lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(new Vector3 (this.path [i] [0], this.path [i] [1],1)));
		}
	
	}

	void MakePoint() {

	}

	int getDir(int a,int b,int diff) {
		if (Mathf.Abs(a-b)<diff) {
			return 0;
		}
		if (a-b<0) {
			return -1;
		} else {
			return 1;
		}
	}

	List<int[]> reducePath(List<int[]> points,int diff=0) {
		List<int[]> result = new List<int[]>();
		int prevdirx = 0;
		int prevdiry = 0;
		for (int i=0; i< points.Count; i++) {
			if (i==0)
				continue;
			int[] prevpoint = points[i-1];
			int[] point = points[i];

			int dirx = this.getDir(point[0],prevpoint[0],diff); 
			int diry = this.getDir(point[1],prevpoint[1],diff); 
			if (diry!=prevdiry || dirx!=prevdirx) {
				//console.log(dirx,diry);
				prevdirx = dirx;
				prevdiry = diry;
				result.Add(prevpoint);
			} 
		}
		return result;
	}


	List<int[]> reducePoints(List<int[]> points,int diff=0 /*draw=false*/) {
		List<int[]> result = new List<int[]> {};
		int prevdirx = 0;
		int prevdiry = 0;
		int mindiff   = 9;
		for (int i = 0; i < points.Count ; i++) {
			if (i==0) 
				continue;
			int[] prevpoint = points[i-1];
			int[] point = points[i];
			int dirx = this.getDir(point[0],prevpoint[0],mindiff); 
			int diry = this.getDir(point[1],prevpoint[1],mindiff); 
			if (diry!=prevdiry || dirx!=prevdirx) {
				prevdirx = dirx;
				prevdiry = diry;
				result.Add(new int[]{dirx,diry});
			} 
		}
		return result;
	}

	void Check() {
		int step_one_reduce_diff = 3;
		int step_two_reduce_diff = 5;
		if (this.path.Count<2) return;
		//List<int[]> result = new List<int[]>{new int[0,0]};

		int[] start = this.path [0];
		int[] end 	= this.path[this.path.Count-1];
		//var result = this.reducePoints(this.path,9,true);        
		//this.points.
		List<int[]> reducedPath = this.reducePath(this.path,step_one_reduce_diff);
		reducedPath.Add(end);

		List<int[]> result = new List<int[]>();
		result.Add(new int[]{0,0});
		result.AddRange(this.reducePoints(reducedPath,step_two_reduce_diff));

		foreach (BaseSpell spell in this.spells) {
			
			if (spell == null || !spell.active) {
				continue;
			}
			bool match = true;
			//Debug.Log (spell.points.Count);
			for (int i=0; i<result.Count; i++) {
				if (result.Count != spell.getPoints().Count) {
					match = false;
					break;
				}


				if (result [i] [0] != (int)spell.getPoints() [i].x) {
					match = false;
					break;
				}

				if (result [i] [1] != (int)spell.getPoints() [i].y) {
					match = false;
					break;
				}
			}
			if (match == true) {
				Instantiate(spell);
				break;
			}
		}
	}

	void Process(int x, int y) {
		int min_diff = 15;
		if (this.path.Count==0) {
			this.path.Add(new int[]{x,y});
			//this.makePoint(x,y);
			return;
		}

		int[] old = this.path[this.path.Count-1];

		if (Mathf.Abs(old[0]-x)+Mathf.Abs(old[1]-y)>min_diff) {
			this.path.Add(new int[]{x,y});
//			this.makePoint(x,y);
		}
		return;
	}
	// Update is called once per frame
	void Update () {
		int left_btn = 0;
		if (!this.active && Input.GetMouseButtonDown (left_btn)) {
			this.Activate ();
		} else if (this.active && Input.GetMouseButtonUp (left_btn)) {
			this.Deactivate ();
		}

		if (!this.active) {
			return;
		}
		int x = (int)Input.mousePosition.x;
		int y = (int)Input.mousePosition.y;
		//var y = this.game.input.y;
		this.Process(x,y);
		this.Draw();
	}
}
