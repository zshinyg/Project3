using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2Controller : MonoBehaviour
{

    public float speed = 5.0f;

    private Rigidbody2D myPlayer;
    private GameObject player;
    private Animator animator;
    //private Vector2 touchOrigin = -Vector2.one;

    //int idle = Animator.StringToHash("Player1Idle");
    //int moveUp = Animator.StringToHash("Player1Up");
    //int runStateHash = Animator.StringToHash("Base Layer.Run");

    public GameObject enemyTarget;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //current = gameObject.transform.position;
        //Debug.Log(animator);
        //Debug.Log(myPlayer);
    }

    //float playerDelta(float a, float b)
    //{
    //    return a - b; 
    //}
    /**
	 * Updates the character position every 0.02 seconds. Controls all movement of the character
	 * @Param None
	 * @Return None
	**/
    void FixedUpdate()
    {
        Vector2 move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        bool isAttacking = CrossPlatformInputManager.GetButtonDown("Attack");

        enemyTarget = GameObject.FindGameObjectWithTag("Enemy");

        //Debug.Log(isAttacking);
        if (move == Vector2.zero)
        {
            //myPlayer.AddForce(-myPlayer.velocity);
        }
        else
        {
            myPlayer.AddForce(move * speed - myPlayer.velocity);
        }


        /// Attack ////
        if (isAttacking)// & !isTriggering)
        {
            animator.Play("Player2Attack_Spin");
            player.GetComponent<IPlayer>().Attack();
        }
        else if (move.x < 0 && (Math.Abs(move.x) > Math.Abs(move.y)))
        {
            //animator.SetTrigger("Player1Left");
            animator.Play("Player2Left");
        }
        else if ((move.x > 0) && (move.x > Math.Abs(move.y)))
        {
            //animator.SetTrigger("Player1Right");
            animator.Play("Player2Right");
        }
        else if (move.y > Math.Abs(move.x) && (move.y > 0))
        {
            //animator.SetTrigger("Player1Up");
            animator.Play("Player2Up");
        }
        else if (-move.y > Math.Abs(move.x) && (move.y < 0))
        {
            //animator.SetTrigger("Player1Down");
            animator.Play("Player2Down");
        }
        else
        {
            animator.SetTrigger("Player2Idle");
            //animator.Play("Player1Idle");
            myPlayer.velocity = Vector3.zero;
        }


    }
}
