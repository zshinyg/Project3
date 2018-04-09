using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter{
	/**
	 * Takes Damage given to the character
	 * @Param int damageTaken
	 * @Return None
	**/
	void TakeDamage (int damageTaken);

	/**
	 * Attacks another character
	 * @Param None
	 * @Return None
	**/
	void Attack ();
	/**
	 * sets the speed of a character
	 * @Param int speed
	 * @Return None
	**/
	void setSpeed (int speed);

	/**
	 * sets the health of a character
	 * @Param int health
	 * @Return None
	**/
	void setHealth(int health);

	/**
	 * Returns if the player is dead or not
	 * @Param None
	 * @Return bool
	**/
	bool isDead ();

	void Die ();

}
