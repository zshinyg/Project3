using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinMan : IEnemy
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
            animator.Play("MuffinManIdle");
        }
        else if (GetComponent<IEnemy>().Health <= 0)
        {
            if (doOnce < 33)
            {
                //Debug.Log("Dead: " + doOnce);
                //animator.Play("MuffinMan_Dead");
                //animation["MuffinMan_Dead"].wrapMode = WrapMode.Once;
                doOnce++;
            }
            else
            {
                animator.Play("MuffinMan_Dead", 0, 0.9f);
            }

        }
        ////// Movement Animations //////
        else if ((current.x > (target.x + 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManLeft");
        }
        else if ((current.x < (target.x - 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManRight");
        }
        else if ((current.y < (target.y - 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManUp");
        }
        else if ((current.y > (target.y + 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManDown");
        }
    }

    public void TakeDamage(int damageTaken)
    {
        animator.SetTrigger("MuffinMan_Hurt");
        Health = Health - damageTaken;
    }

    public void Attack()
    {
        /// Attack Left ////
        if ((current.x > target.x) & (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManAttack_Left");
        }
        //// Attack Right ////
        else if ((current.x < target.x) & (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            animator.SetTrigger("MuffinManAttack_Right");
        }
        /// Attack Up ////
        else if (current.y < target.y)
        {
            animator.SetTrigger("MuffinManAttack_Up");
        }
        /// Attack Down ////
        else if (current.y > target.y)
        {
            animator.SetTrigger("MuffinManAttack_Down");
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
