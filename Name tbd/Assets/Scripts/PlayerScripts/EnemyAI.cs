using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public GameObject Player;					// Player object from heirarchy
	private Transform playerPos;				// Player position
	private Vector3 delta; 						// Distance FROM enemy TO player
	int moveSpeed = 1;
	private bool canMove;
	private int attackDelay;

    MuffinMan muffinMan;


    /* Start
     * @param none
     * @return none
     * Called to start the script and initializes canMove to false
     */
    void Start()
    {
		canMove = false;
		//Debug.Log ("Started");
		attackDelay = 0;
        
        muffinMan = GetComponent<MuffinMan>();
    }


    /**FixedUpdate
	 * Updates every 0.02 seconds prompting the Enemy to follow the main player
	 * @Param None
	 * @Return None
	**/
    void FixedUpdate()
    {
        if (GetComponent<IEnemy>().Health > 0)
        {
            attackDelay++;
            Player = GameObject.FindGameObjectWithTag("Player");
            playerPos = Player.transform;
            delta = playerPos.position - this.transform.position;
            if (!canMove)
            {
                if (Math.Abs(delta.magnitude) < 1)
                {
                    ToggleMove();
                }
            }

            else
            {
                this.transform.position += delta * moveSpeed * Time.deltaTime;

                muffinMan.SetMove(canMove);

                if (attackDelay >= 50)
                {
                    attackDelay = 0;
                    //muffinMan.Attack();

                    //if (GetComponent<IEnemy>().Health > 0)
                    //{
                    TryAttack();
                    //}
                    // else
                    //{
                    //    canMove = false;
                    //}
                }

            }
        }
        else
        {
            canMove = false;
        }
        
    }


	/** TryAttack
	 * Attempts to attack the main player within a certain radius.
	 * @Param None
	 * @Return None
	**/
	public void TryAttack()
    {
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Player") {

                GetComponent<MuffinMan>().Attack();
                hitColliders [i].GetComponent<IPlayer>().TakeDamage (5);
                Debug.Log (hitColliders [i].GetComponent<IPlayer> ().Health);
			} 
			i++;
		}
	}


	public void ToggleMove(){
		canMove = !canMove;
	}
}
