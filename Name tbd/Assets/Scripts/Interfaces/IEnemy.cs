using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour, ICharacter
{


    public int Health = 100;

    int doOnce = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageTaken)
    {
        if (isDead() == false)
        {
            animator.SetTrigger("MuffinMan_Hurt");
            Health = Health - damageTaken;
        }
    }

    public void Attack()
    {

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
            animator.SetTrigger("MuffinMan_Dead");
            doOnce = 1;
            //Destroy(GetComponent<BoxCollider2D>());
        }
        //Destroy (this.gameObject);
        Destroy(GetComponent<BoxCollider2D>());
    }


}
