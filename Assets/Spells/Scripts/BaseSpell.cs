using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : GameElement {
	public string spell_name; 
	public bool active = false;
	public List< Vector2 > points;
	public List< Vector2 > getPoints() {
		return this.points;
	}

}
