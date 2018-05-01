using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	Image HealthBar;
	int maxHealth = 100;
	public static float health;


	// Use this for initialization
	void Start () {
		HealthBar = GetComponent<Image> ();
		health = IPlayer.Health;
	}
	
	// Update is called once per frame
	void Update () {
		HealthBar.fillAmount = health / maxHealth;
	}
}
