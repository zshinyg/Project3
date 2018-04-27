using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour, ICharacter
{
    //MuffinMan muffinMan;
    //Jack jack;

    public int Health = 100;
    int doOnce = 0;

    private Animator animator;
    public string enemyName;

    void Start()
    {
        animator = GetComponent<Animator>();
        //muffinMan = this.GetComponent<MuffinMan>();
        //jack = this.GetComponent<Jack>();

        IEnemy[] scripts = this.GetComponents<IEnemy>();

        foreach (IEnemy ie in scripts)
        {
            Debug.Log(ie.GetType().Name);
            enemyName = ie.GetType().Name;
        }
    }

    //public void SetMove(bool value)
    //{
    //    muffinMan.SetMove(value);
    //    jack.SetMove(value);
    //}

    public void TakeDamage(int damageTaken)
    {
        if (isDead() == false)
        {
            if (enemyName == "MuffinMan")
            {
                animator.SetTrigger("MuffinMan_Hurt");
            }
            else if (enemyName == "Jack")
            {
                animator.SetTrigger("Jack_Hurt");
            }
            
            Health = Health - damageTaken;
        }
    }

    public void Attack()
    {
        //muffinMan.Attack();
        //jack.Attack();
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
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        if (doOnce == 0)
        {
            //Debug.Log("Now Dead");
            if (enemyName == "MuffinMan")
            {
                animator.SetTrigger("MuffinMan_Dead");
            }
            else if (enemyName == "Jack")
            {
                animator.SetTrigger("Jack_Dead");
            }
            doOnce = 1;
            //Destroy(GetComponent<BoxCollider2D>());
        }
        //Destroy (this.gameObject);
        Destroy(GetComponent<BoxCollider2D>());
    }


}
