using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy: MonoBehaviour, ICharacter {


	public int Health = 100;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

	public void TakeDamage (int damageTaken)
    {
        animator.SetTrigger("MuffinMan_Hurt");
        Health = Health - damageTaken;
		isDead ();
	}

	public void Attack ()
    {
	
	}

	public void setSpeed (int speed){
	}

	public void setHealth(int health){
	}

	public bool isDead (){
		if (Health <= 0) {
			Die ();
			return true;
		} else {
			return  false;
		}
	}

	public void Die()
    {
        animator.SetTrigger("MuffinMan_Dead");
        //Destroy (this.gameObject);
	}


}
