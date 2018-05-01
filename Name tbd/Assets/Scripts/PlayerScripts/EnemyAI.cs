using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITestEventSystem {

	public GameObject Player;					// Player object from heirarchy
	private Transform playerPos;				// Player position
	private Vector3 delta; 						// Distance FROM enemy TO player
	float moveSpeed = 1.0f;
	private bool canMove;
	private int attackDelay;

    MuffinMan muffinMan;
    Jack jack;
    public string enemyName;
    IEnemy myEnemy;
    

    /* Awake
     * @param none
     * @return none
     * Called to start the script and initializes canMove to false
     */
    void Awake()
    {
		canMove = false;
        attackDelay = 0;

        IEnemy[] scripts = this.GetComponents<IEnemy>();

        foreach (IEnemy ie in scripts)
        {
            Debug.Log(ie.GetType().Name);
            enemyName = ie.GetType().Name;
            myEnemy = ie;
        }

        if (enemyName == "MuffinMan")
        {
            muffinMan = GetComponent<MuffinMan>();
            moveSpeed = 1.0f;
        }
        else if (enemyName == "Jack")
        {
            jack = GetComponent<Jack>();
            moveSpeed = 0.5f;
        }
    }


    /**FixedUpdate
	 * Updates every 0.02 seconds prompting the Enemy to follow the main player
	 * @Param None
	 * @Return None
	**/
    void FixedUpdate()
    {

        if (GetComponent<IEnemy>().Health > 0)                                  // Health is greater than 0 so continue
        {
            attackDelay++;
            Player = GameObject.FindGameObjectWithTag("Player");
            playerPos = Player.transform;
            delta = playerPos.position - this.transform.position;

            if (!canMove)                                                       // Player is not allowed to move so check if the player is close enouggh
            {
                if (Math.Abs(delta.magnitude) < 1)
                {
                    canMove = true;
                }
            }

            else                                                                // Player allowed to move so move it
            {
                this.transform.position += delta * moveSpeed * Time.deltaTime;

                if (enemyName == "MuffinMan")
                {
                    muffinMan.SetMove(canMove);
                    if (attackDelay >= 50)
                    {
                        attackDelay = 0;
                        TryAttack();

                    }
                }
                else if (enemyName == "Jack")
                {
                    jack.SetMove(canMove);
                    if (attackDelay >= 100)
                    {
                        attackDelay = 0;
                        TryAttack();

                    }
                }

                //muffinMan.SetMove(canMove);

                /*if (attackDelay >= 50)
                {
                    attackDelay = 0;
                    TryAttack();
                    
                }*/

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
			if (hitColliders [i].tag == "Player")
            {
                
                if (enemyName == "MuffinMan")
                {
                    muffinMan.GetComponent<MuffinMan>().Attack();
                    hitColliders[i].GetComponent<IPlayer>().TakeDamage(5);
                }
                else if (enemyName == "Jack")
                {
                    jack.GetComponent<Jack>().Attack();
                    hitColliders[i].GetComponent<IPlayer>().TakeDamage(10);
                }
                
                //muffinMan.GetComponent<MuffinMan>().Attack();

                //hitColliders [i].GetComponent<IPlayer>().TakeDamage (5);
                //Debug.Log (hitColliders [i].GetComponent<IPlayer> ().Health);
			} 
			i++;
		}
	}


    public void StartTest()
    {
        Debug.Log("Recieved message");
        this.canMove = true;
    }

    public void EndTest()
    {
   
    }
}
