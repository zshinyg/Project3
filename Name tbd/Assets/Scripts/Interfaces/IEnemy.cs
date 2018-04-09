using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy: MonoBehaviour, ICharacter {


	public int Health = 100;

	void Start(){

	}

	public void TakeDamage (int damageTaken){
		Health = Health - damageTaken;
		if (isDead ()) {
			Destroy (this.gameObject);
		}
	}

	public void Attack (){
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		if (Health <= 0) {
			return true;
		} else {
			return  false;
		}
	}

	public void Die(){
	}


}
