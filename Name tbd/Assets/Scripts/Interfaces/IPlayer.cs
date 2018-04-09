using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer: ICharacter {

	public Transform myPlayer;
	public Transform Enemy;

	public void Start()
	{


	}

	public void TakeDamage (int damageTaken){

	}

	public void Attack<T>(T component)
	{
		myPlayer = GameObject.FindGameObjectWithTag ("Player").transform;
		Enemy = GameObject.FindGameObjectWithTag ("Enemy").transform;
		if(Math.Abs(Vector3.Distance(myPlayer.position, Enemy.position)) <= 0.5)
		{
			IEnemy myEnemy = component as IEnemy;
			myEnemy.TakeDamage(5);

		}
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		return false;
	}



}

