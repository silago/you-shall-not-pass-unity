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
	public float	_damage	= 50	;
	public bool	_destroyable = true	;




	public void init() {
		//this.gameObject.AddComponent<Rigidbody2D>;
		return;
	}

	public void Die() {
		Destroy (this.gameObject);
	}

	/*
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("col");
		if (coll.gameObject.tag != this.gameObject.tag) {
			coll.gameObject.SendMessage("GetHit", this._damage);
		}
	}
	*/

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log ("trig");
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
	
	// Update is called once per frame
}
