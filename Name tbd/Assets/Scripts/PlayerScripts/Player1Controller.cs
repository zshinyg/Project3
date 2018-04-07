using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player1Controller : MonoBehaviour
{

    public float speed = 5.0f;

    private Rigidbody2D myPlayer;

    private Animator animator;
    //private Vector2 touchOrigin = -Vector2.one;

    //int idle = Animator.StringToHash("Player1Idle");
    //int moveUp = Animator.StringToHash("Player1Up");
    //int runStateHash = Animator.StringToHash("Base Layer.Run");


    void Start()
    {
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

    void FixedUpdate()
    {
        Vector2 move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        bool isAttacking = CrossPlatformInputManager.GetButtonDown("Attack");
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
            //animator.SetTrigger("Player1Attack");
            animator.Play("Player1Attack_Spin");
            //CrossPlatformInputManager.Update();
            Debug.Log(isAttacking);
        }
        else if (move.x < 0 && (Math.Abs(move.x) > Math.Abs(move.y)))
        {
            //animator.SetTrigger("Player1Left");
            animator.Play("Player1Left");
        }
        else if ((move.x > 0) && (move.x > Math.Abs(move.y)))
        {
            //animator.SetTrigger("Player1Right");
            animator.Play("Player1Right");
        }
        else if (move.y > Math.Abs(move.x) && (move.y > 0))
        {
            //animator.SetTrigger("Player1Up");
            animator.Play("Player1Up");
        }
        else if (-move.y > Math.Abs(move.x) && (move.y < 0))
        {
            //animator.SetTrigger("Player1Down");
            animator.Play("Player1Down");
        }
        else
        {
            animator.SetTrigger("Player1Idle");
            //animator.Play("Player1Idle");
            myPlayer.velocity = Vector3.zero;
        }

        //float moveVertical = Input.GetAxis("Vertical");
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //myPlayer.AddForce(movement);
        //animator.SetFloat("Speed", moveHorizontal);

        /*
        //// Up ////
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {
            animator.SetTrigger("Player1Up");
            pos += Vector3.up;
        }

        //// Down ////
        else if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {
            animator.SetTrigger("Player1Down");
            pos += Vector3.down;

        }

        //// Left ////
        else if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {
            animator.SetTrigger("Player1Left");
            pos += Vector3.left;
            //animator.SetTrigger("Player1Idle");
        }

        //// Right ////
        else if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {
            animator.SetTrigger("Player1Right");
            pos += Vector3.right;
            //animator.SetTrigger("Player1Idle");
        }
        else
        {
            //animator.SetTrigger("Player1Idle");
        }

        //// Attack ////
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Player1Attack");
        }
        */

        /*if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("Player1Idle");
            //moveVertical = 0;
            //moveHorizontal = 0;
        }*/

        //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);


        //// May need for Mobile Controls!!!! ////
        
        /*int horizontal = 0;
        int vertical = 0;

        //Check if we are running either in the Unity editor or in a standalone build.
        #if UNITY_STANDALONE || UNITY_WEBPLAYER

        horizontal = (int) (Input.GetAxisRaw ("Horizontal"));

        vertical = (int) (Input.GetAxisRaw ("Vertical"));

        if(horizontal != 0)
        {
            vertical = 0;
        }
        
        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        //Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {
                //Store the first touch detected.
                Touch myTouch = Input.touches[0];
                
                //Check if the phase of that touch equals Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
                    touchOrigin = myTouch.position;
                }
                
                //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    //Set touchEnd to equal the position of this touch
                    Vector2 touchEnd = myTouch.position;
                    
                    //Calculate the difference between the beginning and end of the touch on the x axis.
                    float x = touchEnd.x - touchOrigin.x;
                    
                    //Calculate the difference between the beginning and end of the touch on the y axis.
                    float y = touchEnd.y - touchOrigin.y;
                    
                    //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                    touchOrigin.x = -1;
                    
                    //Check if the difference along the x axis is greater than the difference along the y axis.
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                        horizontal = x > 0 ? 1 : -1;
                    else
                        //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                        vertical = y > 0 ? 1 : -1;
                }
            }
        #endif*/
    }
}
