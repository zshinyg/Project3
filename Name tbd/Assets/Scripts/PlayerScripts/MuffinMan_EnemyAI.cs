using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//// Class for Box Collision ////
internal class MuffinMan_Attack
{
    private GameObject myPlayer;
    private bool hasCollided;

    // Use this for initialization
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        hasCollided = false;

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == myPlayer)
        {
            hasCollided = true;
        }
        Debug.Log(hasCollided);
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == myPlayer)
        {
            hasCollided = false;
        }
        Debug.Log(hasCollided);
    }

    public bool IsColliding()
    {
        return hasCollided;
    }

    // Update is called once per frame
    //void Update () {
    //	
    //}
}


//// Main class for the AI ////
public class MuffinMan_EnemyAI : MovingObject
{
    private Transform player;               // Reference to the player's position.
    private Transform enemy;
    public float speed = 0.1f;
    private Rigidbody2D myEnemy;
    private BoxCollider2D myCollider;
    private GameObject myPlayer;

    public float deltaX;
    public float deltaY;

    private Animator animator;
    public Vector3 current;
    public Vector3 target;

    private bool skipMove;
    public bool startMoving = false;

    MuffinMan_Attack attackLeft;

    void OnTriggerEnter2D(BoxCollider2D other)
    {
        BoxCollider2D newCollider = GetComponent<BoxCollider2D>();
        if (other.gameObject == myPlayer)
        {
            animator.Play("MuffinManAttack_Right");
        }
        
    }

    void OnTriggerExit2D(BoxCollider2D other)
    {
        BoxCollider2D newCollider = GetComponent<BoxCollider2D>();
        if (other.gameObject == myPlayer)
        {
            animator.Play("MuffinManAttack_Right");
        }

    }

    private void Attack()
    {
        if (attackLeft.IsColliding())
        {
            
        }
    }



    // Use this for initialization
    protected override void Start ()
    {
        //GameManager.instance.AddEnemyToList(this);

        animator = GetComponent<Animator>();
        myEnemy = GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        //myPlayer = GameObject.FindGameObjectWithTag("Player");

        myCollider = GetComponent<BoxCollider2D>();
        
        base.Start();
    }


    void FixedUpdate ()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        target = player.position;
        current = enemy.position;
        deltaX = target.x - current.x;
        deltaY = target.y - current.y;

        MoveEnemy();

        //OnTriggerEnter2D(myCollider);
        //Attack();

    }

    void Awake()
    {
        // Set up the references.
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //nav = GetComponent<NavMeshAgent>();
    }


    protected override void AttemptMove<T>(float xDir, float yDir)
    {
        //Debug.Log(Vector3.Distance(current, target));
        //Debug.Log(xDir);
        //Debug.Log(yDir);

        //if (Math.Abs(Vector3.Distance(current, target)) > 1.5f)
        //{
            if(Mathf.Abs(xDir) > Mathf.Abs(yDir))
            {
                float xOffset = target.x < current.x ? 1.40f : -1.40f;
                base.AttemptMove<T>(xDir + xOffset, yDir);
                //myEnemy.velocity = Vector3.zero;
                return;
                //myEnemy.AddForce(new Vector2(speed,speed));
                //base.AttemptMove<T>(Mathf.Abs(target.x - 1), Mathf.Abs(target.y - 1));
                
            }
            else
            {
                float yOffset = target.y < current.y ? 1.40f : -1.40f;
                base.AttemptMove<T>(xDir, yDir + yOffset);
                //myEnemy.velocity = Vector3.zero;
                return;
                //myEnemy.AddForce(new Vector2(speed, speed));
            }
            
        //Call the AttemptMove function from MovingObject.
        //base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        /*float xPos = 0.0f;
        float yPos = 0.0f;

        yPos = deltaY;
        xPos = deltaX;*/
        Debug.Log(Vector3.Distance(current, target));
        if (Math.Abs(Vector3.Distance(current, target)) >= 20.0f)
        {
            //enemy.Translate(new Vector3(current.x, current.y, 0));
            //myEnemy.velocity = Vector3.zero;
            //myEnemy.AddForce(-myEnemy.velocity);
            if(startMoving == false)
            {
                animator.Play("MuffinManIdle");
            }
            
        }
        else if (!(Math.Abs(Vector3.Distance(current, target)) <= 1.5f))
        {
            startMoving = true;

            AttemptMove<Player1Controller>(deltaX, deltaY);
            if (myCollider.gameObject == myPlayer)
            {
                Debug.Log("Collided!");
            }

            ////// Movement Animations //////
            if ((current.x > (target.x + 1.6f)) && (Math.Abs(deltaX) > Math.Abs(deltaY)))
            {
                if (myCollider.gameObject == myPlayer)
                {
                    Debug.Log("Collided!");
                }
                //Debug.Log(current.x);
                //Debug.Log(target.x + 1.5f);
                //animator.SetTrigger("MuffinManLeft");
                animator.Play("MuffinManLeft");
            }
            else if ((current.x < (target.x - 1.6f)) && (Math.Abs(deltaX) > Math.Abs(deltaY)))
            {
                //animator.SetTrigger("MuffinManRight");
                animator.Play("MuffinManRight");

                //OnTriggerEnter2D(myCollider);
            }
            else if ((current.y < (target.y - 2.0f)) && (Math.Abs(deltaX) < Math.Abs(deltaY)))
            {
                //animator.SetTrigger("MuffinManUp");
                animator.Play("MuffinManUp");
            }
            else if ((current.y > (target.y + 2.0f)) && (Math.Abs(deltaX) < Math.Abs(deltaY)))
            {
                //animator.SetTrigger("MuffinManDown");
                animator.Play("MuffinManDown");
            }
            /// Attack Left ////
            else if (current.x > (target.x))
            {
                animator.Play("MuffinManAttack_Left");
            }
            //// Attack Right ////
            else if (current.x < (target.x))
            {
                animator.SetTrigger("MuffinManAttack_Right");
                //Debug.Log(isAttackingRight);
            }
            /// Attack Up ////
            else if ((current.y < (target.y)) & (current.y == target.y))
            {
                animator.Play("MuffinManAttack_Up");
                //Debug.Log(isAttackingUp);
            }
            /// Attack Down ////
            else if ((current.y > (target.y)) & (current.y == target.y))
            {
                animator.Play("MuffinManAttack_Down");
                //Debug.Log(isAttackingDown);
            }
            return;
        }
        ////// Attack Animations //////
        else
        {
            /// Attack Left ////
            if (current.x > (target.x + 1.4f))
            {
                animator.Play("MuffinManAttack_Left");
            }
            //// Attack Right ////
            else if (current.x < (target.x - 1.4f))
            {
                animator.SetTrigger("MuffinManAttack_Right");
                //Debug.Log(isAttackingRight);
            }
            /// Attack Up ////
            else if ((current.y < (target.y - 1.6f)) & (current.y == target.y))
            {
                animator.Play("MuffinManAttack_Up");
                //Debug.Log(isAttackingUp);
            }
            /// Attack Down ////
            else if ((current.y > (target.y + 1.6f)) & (current.y == target.y))
            {
                animator.Play("MuffinManAttack_Down");
                //Debug.Log(isAttackingDown);
            }
        }

        ////// Movement Animations //////
        /*if ((current.x > (target.x + 1.5f)) && (Math.Abs(deltaX) > Math.Abs(deltaY) ))
        {
            if (myCollider.gameObject == myPlayer)
            {
                Debug.Log("Collided!");
            }
                //Debug.Log(current.x);
            //Debug.Log(target.x + 1.5f);
            //animator.SetTrigger("MuffinManLeft");
            animator.Play("MuffinManLeft");
        }
        else if ((current.x < (target.x - 1.5f)) && (Math.Abs(deltaX) > Math.Abs(deltaY) ))
        {
            //animator.SetTrigger("MuffinManRight");
            animator.Play("MuffinManRight");

            //OnTriggerEnter2D(myCollider);
        }
        else if ((current.y < (target.y - 1.5f)) && (Math.Abs(deltaX) < Math.Abs(deltaY) ))
        {
            //animator.SetTrigger("MuffinManUp");
            animator.Play("MuffinManUp");
        }
        else if ((current.y > (target.y + 1.5f)) && (Math.Abs(deltaX) < Math.Abs(deltaY) ))
        {
            //animator.SetTrigger("MuffinManDown");
            animator.Play("MuffinManDown");
        }
        /*else
        {
            animator.SetTrigger("MuffinManIdle");
            //animator.Play("Player1Idle");
        }*/


    }

    protected override void OnCantMove<T>(T component)
    {
        //Declare hitPlayer and set it to equal the encountered component.
        Player1Controller hitPlayer = component as Player1Controller;

        //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
        //hitPlayer.LoseFood(playerDamage);

        //Set the attack trigger of animator to trigger Enemy attack animation.
        //animator.SetTrigger("MuffinManAttack_Left");
        //if (hitPlayer.collider.tag == "Player") { 
        //animator.Play("MuffinManAttack_Left");
        /// Attack Left ////
        /*if ((current.x >= target.x))
        {
            animator.Play("MuffinManAttack_Left");
        }
        //// Attack Right ////
        else if ((current.x <= target.x))
        {
            animator.SetTrigger("MuffinManAttack_Right");
            //Debug.Log(isAttackingRight);
        }
            /// Attack Up ////
        else if ((current.y < 0) & (current.y == target.y))
        {
            animator.Play("MuffinManAttack_Up");
            //Debug.Log(isAttackingUp);
        }
            /// Attack Down ////
        else if ((current.y > 0) & (current.y == target.y))
        {
            animator.Play("MuffinManAttack_Down");
            //Debug.Log(isAttackingDown);
        }

       // }*/
    }


}
