using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IPlayer: MonoBehaviour, ICharacter, IEventSystemHandler
{
	public GameObject[] Enemies;
	public  static int Health;
    public bool Invinc;
    public int InvincDur;
    public int Icount;
    public int Attak;
    public int AttakDur;
    public int Acount;
	Image healthBar;
	public int maxHealth = 100;


    private Animator animator;

    public void Start()
	{
        animator = GetComponent<Animator>();
        setHealth(100);
        setAttack(20);
        setInvincibility(false);
        setADuration(0);
        setIDuration(0);
        Icount = 0;
        Acount = 0;
		healthBar = GetComponent<Image> ();
		Health = maxHealth;

    }

	public void TakeDamage (int damageTaken)
    {
        if (!Invinc)
        {
            animator.SetTrigger("Player1_Hurt");
            Health = Health - damageTaken;
            Debug.Log("My Health:"+Health);
            if (isDead())
            {
                Die();
            }
        }
        else { Debug.Log("My Health:"+Health); }
	}

	public void Attack()
	{
		//Enemies = GameObject.FindGameObjectsWithTag ("Enemies");
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
		int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders [i].tag == "Enemy") {
				
				hitColliders [i].GetComponent<IEnemy> ().TakeDamage(Attak);
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
        Health = health;
        Debug.Log("Health:"+Health);
	}

    public int getHealth()
    {
        return Health;
    }

    public void setAttack(int attack)
    {
        Attak = attack;
        Debug.Log("Attack:"+Attak);
    }

    public int getAttack()
    {
        return Attak;
    }

    public void setInvincibility(bool invinc)
    {
        Invinc = invinc;
        Debug.Log("Invincible:"+ Invinc);
    }

    public void setIDuration(int dur)
    {
        InvincDur = (dur*50);
        Debug.Log("Invincibility Duration:" + InvincDur);
    }

    public int getIDuration()
    {
        return InvincDur;
    }

    public void setADuration(int dur)
    {
        AttakDur = (dur*50);
        Debug.Log("Attack Duration:" + AttakDur);
    }

    public int getADuration()
    {
        return AttakDur;
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
        animator.Play("Player1_Dead", 0, 0.9f);
        ExecuteEvents.Execute<IGameEventSystem>(GameObject.Find("GameManager"), null, (x, y) => x.GameOver());
        //animator.enabled = false;
        //Destroy (this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<IItem>().Ability(this);
            
        }
    }

    void FixedUpdate()
    {
		healthBar.fillAmount = Health / maxHealth;
        if (getIDuration() > 0)
        {
            if (Icount <= getIDuration())
            {
                Icount = Icount + 1;
            }
            else
            {
                setInvincibility(false);
                setIDuration(0);
                Icount = 0;
            }
        }
        if (getADuration() > 0)
        {
            if (Acount <= getADuration())
            {
                Acount = Acount + 1;
            }
            else
            {
                setAttack(20);
                setADuration(0);
                Acount = 0;
            }
        }
    }
}

