using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITestEventSystem {

	public GameObject Player;					// Player object from heirarchy
	private Transform playerPos;				// Player position
	private Vector3 delta; 						// Distance FROM enemy TO player
	int moveSpeed = 1;
	private bool canMove;
	private int attackDelay;

    MuffinMan muffinMan;
    Jack jack;
    public string enemyName;
    IEnemy myEnemy;
    

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

        IEnemy[] scripts = this.GetComponents<IEnemy>();

        foreach (IEnemy ie in scripts)
        {
            Debug.Log(ie.GetType().Name);
            enemyName = ie.GetType().Name;
            myEnemy = ie;
        }

        if (enemyName == "MuffinMan")
        {
            //myEnemy = this.GetComponent<MuffinMan>();
            muffinMan = GetComponent<MuffinMan>();
        }
        else if (enemyName == "Jack")
        {
            //myEnemy = this.GetComponent<Jack>();
            jack = GetComponent<Jack>();
        }
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
                    canMove = true;
                }
            }

            else
            {
                this.transform.position += delta * moveSpeed * Time.deltaTime;

                if (enemyName == "MuffinMan")
                {
                    muffinMan.SetMove(canMove);
                }
                else if (enemyName == "Jack")
                {
                    jack.SetMove(canMove);
                }

                //muffinMan.SetMove(canMove);

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
			if (hitColliders [i].tag == "Player")
            {
                
                if (enemyName == "MuffinMan")
                {
                    muffinMan.GetComponent<MuffinMan>().Attack();
                }
                else if (enemyName == "Jack")
                {
                    jack.GetComponent<Jack>().Attack();
                }
                
                //muffinMan.GetComponent<MuffinMan>().Attack();

                hitColliders [i].GetComponent<IPlayer>().TakeDamage (5);
                Debug.Log (hitColliders [i].GetComponent<IPlayer> ().Health);
			} 
			i++;
		}
	}


    public void StartTest()
    {
        canMove = true;
    }

}
