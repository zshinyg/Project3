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
	private int attackDelay;

    MuffinMan muffinMan;

    void Start()
    {
		canMove = false;
		//Debug.Log ("Started");
		attackDelay = 0;
        
        muffinMan = GetComponent<MuffinMan>();
    }


	/**
	 * Updates every 0.02 seconds prompting the Enemy to follow the main player
	 * @Param None
	 * @Return None
	**/
	void FixedUpdate()
    {
		attackDelay++;
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = Player.transform;
		delta = playerPos.position - this.transform.position;

        if (Math.Abs(delta.magnitude) < 1)
        {
            canMove = true;
            this.transform.position += delta * moveSpeed * Time.deltaTime;
		

        muffinMan.SetMove(canMove);

        if (attackDelay >= 50)
        {
			attackDelay = 0;
            //muffinMan.Attack();
            tryAttack();
		}

        }
        else
        {
            canMove = false;
        }
    }


	/**
	 * Attempts to attack the main player within a certain radius.
	 * @Param None
	 * @Return None
	**/
	public void tryAttack()
    {
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Player") {

                GetComponent<MuffinMan>().Attack();
                hitColliders [i].GetComponent<IPlayer> ().TakeDamage (5);
                Debug.Log (hitColliders [i].GetComponent<IPlayer> ().Health);
			} 
			i++;
		}
	}


	public void toggleMove(){
		canMove = !canMove;
	}
}
