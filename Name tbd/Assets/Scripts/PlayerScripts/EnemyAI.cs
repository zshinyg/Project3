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

    private Animator animator;
    public Vector3 current;
    public Vector3 target;

    void Start()
    {
		canMove = false;
		//Debug.Log ("Started");
		attackDelay = 0;

        animator = GetComponent<Animator>();
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

        current = transform.position;
        target = playerPos.position;

        if (Math.Abs(delta.magnitude) < 1)
        {
            canMove = true;
            this.transform.position += delta * moveSpeed * Time.deltaTime;
		}
        else
        {
            canMove = false;
        }

        if (canMove == false)
        {
            animator.Play("MuffinManIdle");
        }

        ////// Movement Animations //////
        else if ((current.x > (target.x + 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManLeft");
            animator.Play("MuffinManLeft");
        }
        else if ((current.x < (target.x - 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManRight");
            animator.Play("MuffinManRight");

            //OnTriggerEnter2D(myCollider);
        }
        else if ((current.y < (target.y - 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManUp");
            animator.Play("MuffinManUp");
        }
        else if ((current.y > (target.y + 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManDown");
            animator.Play("MuffinManDown");
        }


        if (attackDelay >= 50)
        {
			attackDelay = 0;
			tryAttack();
		}
			
	}


	/**
	 * Attempts to attack the main player within a certain radius.
	 * @Param None
	 * @Return None
	**/
	public void tryAttack(){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Player") {

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
