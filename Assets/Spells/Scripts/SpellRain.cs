using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRain : BaseSpell {
		public int cost  = 2;
		public int _speed = 300;


		SpriteRenderer spriteRenderer;
		Rigidbody2D rigidbody2D;

		void Start () {
			spriteRenderer 	= GetComponent<SpriteRenderer>();
			rigidbody2D 	= GetComponent<Rigidbody2D>();		
			Cast ();
		}


		public void Cast () {
			//this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Time.deltaTime*this._speed, 0);
		}

		void Update () {
			this._health -= Time.deltaTime;
			if (this._health < 0) {
				this.Die();
			}
		}
	}