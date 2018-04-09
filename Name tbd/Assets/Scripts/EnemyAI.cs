using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour {

	public GameObject Player;					// Player object from heirarchy
	private Transform playerPos;				// Player position
	private Vector3 delta; 						// Distance FROM enemy TO player
	int moveSpeed = 1;
	private bool canMove;

	void Start(){
		canMove = false;
		Debug.Log ("Started");
	}

	void Update(){
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = Player.transform;
		delta = playerPos.position - this.transform.position;



		if (Math.Abs(delta.magnitude) < 1) {


			this.transform.position += delta * moveSpeed * Time.deltaTime;
		}



//		if(Vector3.Distance(transform.position,Player.) >= minDist){
//			transform.position += transform.forward * moveSpeed * Time.deltaTime;
//
//			if(Vector3.Distance(transform.position, Player.position) <= maxDist){
//				//attack
//			}
//		}
	}


	public void toggleMove(){
		canMove = !canMove;
	}
}
