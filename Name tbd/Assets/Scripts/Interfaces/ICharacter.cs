using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter{

	void TakeDamage (int damageTaken);

	void Attack<T> (T component);

	void setSpeed (int speed);

	void setHealth(int health);

	bool isDead ();

}
