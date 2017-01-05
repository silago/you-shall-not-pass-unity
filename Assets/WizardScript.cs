using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public in

public class WizardScript : MonoBehaviour {
	public int health = 100;
	public int mana_max = 10;
	public int mana = 10;
	// Use this for initialization
	void Start () {
		Debug.Log("started");
	}


	// Update is called once per frame
	void Update () {
		
	}
}


/*
 * 'use strict';
var Wizard = function(game,x,y,resource) {
    Phaser.Sprite.call(this, game,x,y,resource);
    game.physics.enable(this, Phaser.Physics.ARCADE);
    this.health =  100;
    this.mana_max =  10;
    this.mana =  this.mana_max;
    //this.constructor.prototype.mana_regain = 20;

    this.init =function() {
        this.gui.health.setText(this.health);
        this.gui.mana.setText(parseInt(this.mana));
    }

    this.gui = {};
    this.getHit = function(damage) {
        this.health-=damage;
        this.gui.health.setText(this.health);
        if (this.health<0) {
          console.log(this.game.state);
          this.game.state.start('dead');
        }
    }
    this.gui.health = game.add.text(130, 60, this.health, {
            font: "65px Arial",
            fill: "#ff0044",
            align: "center"
        });
    this.gui.mana = game.add.text(130, 160, parseInt(this.mana), {
            font: "65px Arial",
            fill: "#4400ff",
            align: "center"
        });
    //this.defaulSpell = new Dot(game,this,{enemies:enemies});
    this.takeMana = function(v) {
      if (this.mana<v) return false;
      this.mana-=v;
      this.gui.mana.setText(parseInt(this.mana));
      return true;
    }
    var self = this;
    this.update = function() {
      var t = 0.001*game.time.elapsed;
      if (this.mana<10) {
          this.mana = this.mana + this.mana_regain*t;
          this.gui.mana.setText(parseInt(this.mana));
      } else if (this.mana>this.mana_max) {
          this.mana = this.mana_max;
          this.gui.mana.setText(parseInt(this.mana));
      }
    }

    game.add.existing(this);
    this.immovable = true;
    this.body.moves = false;


    this.inventory = [];

}
Wizard.prototype = Object.create(Phaser.Sprite.prototype);
Wizard.prototype.mana_regain = 1;//Object.create(Phaser.Sprite.prototype);

export default Wizard;


*/