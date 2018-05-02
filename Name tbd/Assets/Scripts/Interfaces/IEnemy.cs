using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy: MonoBehaviour, ICharacter {


	public int Health = 100;

	void Start(){

	}

	public void TakeDamage (int damageTaken){
		Health = Health - damageTaken;
		isDead ();
	}

	public void Attack (){
	
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		if (Health <= 0) {
			Die ();
			return true;
		} else {
			return  false;
		}
	}

	public void Die(){
		Destroy (this.gameObject);
	}


}
