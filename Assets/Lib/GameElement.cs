using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;
using System.IO;

public static class CustomAssetUtility
{
	public static void CreateAsset<T> () where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();

		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension (path) != "")
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset (asset, assetPathAndName);

		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}


public class InventoryItemAssetCreator : MonoBehaviour {
	[MenuItem("Assets/Create/GameElementObject")]
	public static void CreateAsset()
	{
		CustomAssetUtility.CreateAsset<GameElementObject>();
	}
}

public class GameElementObject : ScriptableObject {
	public Sprite Sprite;
	public MonoScript Script;
	public float	_health	= 100	;
	public float	_damage	= 50	;
	public float    _cost   = 1		; 
	public bool	_destroyable = true	;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class GameElement : MonoBehaviour {

	public float	_health	= 100	;
	public float    _cost   = 1		; 
	public float	_damage	= 50	;
	public bool	_destroyable = true	;

	public float _hit_interval = 3;
	public float _hit_interval_timer = 0.1f;

	Dictionary<int,Collision2D> collision_colliders = new Dictionary<int,Collision2D>() ;


	public void init() {
		//this.gameObject.AddComponent<Rigidbody2D>;
		return;
	}

	public virtual void Die() {
		Destroy (this.gameObject);
	}


	void OnCollisionExit2D(Collision2D coll) {
		if (this.collision_colliders.ContainsKey (coll.gameObject.GetInstanceID ())) {
			this.collision_colliders.Remove (coll.gameObject.GetInstanceID ());
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		CollisionHit (coll);

		if (!this.collision_colliders.ContainsKey (coll.gameObject.GetInstanceID ())) {
			this.collision_colliders.Add (coll.gameObject.GetInstanceID (), coll);
		}
	}
		

	void CollisionHit(Collision2D coll) {
		coll.gameObject.SendMessage("GetHit", this._damage);
	}


	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("trig");
		if (coll.gameObject.tag != this.gameObject.tag) {
			Debug.Log (coll.gameObject.name);
			if (coll!=null)
			coll.gameObject.SendMessage("GetHit", this._damage);
		}
	}

	public void GetHit(int damage) {
		//Debug.Log (_health);
		if (this._destroyable) {
			this._health -= damage;
		}
		if (this._health <= 0) {
			this.Die ();
		}
		//Debug.Log (this._health);
	}
	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		_hit_interval_timer -= Time.deltaTime;
		if (_hit_interval_timer <= 0) {
			_hit_interval_timer = _hit_interval;

			foreach (KeyValuePair<int,Collision2D> kv in this.collision_colliders) {
				if (kv.Value.gameObject == null)
					continue;
				CollisionHit(kv.Value);
			}
		}
	}
	
	// Update is called once per frame
}
