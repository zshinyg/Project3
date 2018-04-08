using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer: ICharacter {

	public void TakeDamage (int damageTaken){

	}

	public int GiveDamage (){
		return 10;
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		return false;
	}


}

