using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinMan : IEnemy
{

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
        else if (Health <= 0)
        {
            animator.Play("MuffinMan_Dead");
        }
        ////// Movement Animations //////
        else if ((current.x > (target.x + 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManLeft");
            animator.SetTrigger("MuffinManLeft");
        }
        else if ((current.x < (target.x - 0.4f)) && (Math.Abs(delta.x) > Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManRight");
            animator.SetTrigger("MuffinManRight");
        }
        else if ((current.y < (target.y - 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManUp");
            animator.SetTrigger("MuffinManUp");
        }
        else if ((current.y > (target.y + 0.5f)) && (Math.Abs(delta.x) < Math.Abs(delta.y)))
        {
            //animator.SetTrigger("MuffinManDown");
            animator.SetTrigger("MuffinManDown");
        }
    }

	public void TakeDamage (int damageTaken)
    {
        animator.SetTrigger("MuffinMan_Hurt");
        Health = Health - damageTaken;
    }

	public void Attack ()
    {
        /// Attack Left ////
        if (current.x > (target.x))
        {
            animator.SetTrigger("MuffinManAttack_Left");
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
            animator.SetTrigger("MuffinManAttack_Up");
            //Debug.Log(isAttackingUp);
        }
        /// Attack Down ////
        else if ((current.y > (target.y)) & (current.y == target.y))
        {
            animator.SetTrigger("MuffinManAttack_Down");
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
		if (Health <= 0)
        {
            return true;
		}
        else
        {
			return  false;
		}
	}


}
