using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : IEnemy
{
    public int Health;

    private Animator animator;
    private Vector3 delta;
    public Vector3 current;
    public Vector3 target;
    private bool letMove;

    int doOnce = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        letMove = false;
    }

    public void SetMove(bool value)
    {
        letMove = value;
    }

    bool GetMove()
    {
        return letMove;
    }

    public void FixedUpdate()
    {
        current = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;

        delta = target - this.transform.position;

        if (letMove == false)
        {
            animator.Play("JackIdle");
        }
        else if (GetComponent<IEnemy>().Health <= 0)
        {
            if (doOnce < 33)
            {
                //Debug.Log("Dead: " + doOnce);
                //animator.Play("Jack_Dead");
                //animation["Jack_Dead"].wrapMode = WrapMode.Once;
                doOnce++;
            }
            else
            {
                animator.Play("Jack_Dead", 0, 0.9f);
            }

        }
        ////// Movement Animations //////
        else if ((current.x > (target.x + 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackLeft");
        }
        else if ((current.x < (target.x - 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackRight");
        }
        else if ((current.y < (target.y - 0.6f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackUp");
        }
        else if ((current.y > (target.y + 0.6f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackDown");
        }
    }

    public void TakeDamage(int damageTaken)
    {
        animator.SetTrigger("Jack_Hurt");
        Health = Health - damageTaken;
    }

    public void Attack()
    {
        /// Attack Left ////
        if ((current.x > target.x) & (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackAttack_Left");
        }
        //// Attack Right ////
        else if ((current.x < target.x) & (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("JackAttack_Right");
        }
        /// Attack Up ////
        else if (current.y < target.y)
        {
            animator.SetTrigger("JackAttack_Up");
        }
        /// Attack Down ////
        else if (current.y > target.y)
        {
            animator.SetTrigger("JackAttack_Down");
        }
    }

    public void setSpeed(int speed)
    {
    }

    public void setHealth(int health)
    {
    }

    public bool isDead()
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
