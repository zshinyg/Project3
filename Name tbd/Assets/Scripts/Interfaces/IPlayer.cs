using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer: MonoBehaviour, ICharacter
{
	public GameObject[] Enemies;

	public int Health = 100;

    private Animator animator;

    public void Start()
	{
        animator = GetComponent<Animator>();
    }

	public void TakeDamage (int damageTaken)
    {
        animator.SetTrigger("Player1_Hurt");
        Health = Health - damageTaken;
		if (isDead ())
        {
			Die ();
		}
	}

	public void Attack()
	{
		//Enemies = GameObject.FindGameObjectsWithTag ("Enemies");
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders [i].tag == "Enemy") {
				
				hitColliders [i].GetComponent<IEnemy> ().TakeDamage(20);
				Debug.Log(hitColliders [i].GetComponent<IEnemy> ().Health);
			} 
			i++;

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

	public void Die ()
    {
        animator.SetTrigger("Player1_Dead");
        //Destroy (this.gameObject);
    }



}

