using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer {

	void DealDamage (IEnemy enemy);

	void TakeDamage ();

	void Die ();

	void Controller();

	void setAttack (int attackStrength);

	int getAttack ();

	void setHealth (int strength);

	int getHealth ();

	int setInvincibility (bool isInvinc);

}

