using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MuffinMan : IEnemy{

	public int Health = 100;

    private Animator animator;
    private Vector3 delta;
    public Vector3 current;
    public Vector3 target;
    private bool letMove;

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

    void FixedUpdate()
    {
        current = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;

        delta = target - this.transform.position;

        if (letMove == false)
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
    }

	public void TakeDamage (int damageTaken)
    {
		Health = Health - damageTaken;
	}

	public void Attack ()
    {
        /// Attack Left ////
        if (current.x > (target.x))
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
    }

	public void setSpeed (int speed)
    {
	}

	public void setHealth(int health)
    {
	}

	public bool isDead ()
    {
		if (Health <= 0) {
			return true;
		} else {
			return  false;
		}
	}


}
