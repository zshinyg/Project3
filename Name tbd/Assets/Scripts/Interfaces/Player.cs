using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer {

	void DealDamage (IEnemy enemy);

	void TakeDamage ();

	void Die ();

	void Controller();

}

