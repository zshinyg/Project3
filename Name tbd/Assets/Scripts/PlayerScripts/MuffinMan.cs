using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinMan : IEnemy{

	public int Health = 100;

	void Start(){

	}

	public void TakeDamage (int damageTaken){
		Health = Health - damageTaken;
	}

	public void Attack (){
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		return false;
	}


}
