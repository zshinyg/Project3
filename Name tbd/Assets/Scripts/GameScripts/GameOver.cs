using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {


	/**
	 * Loads MainMenu
	 * @Param None
	 * @Return None
	**/
	public void toMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
		
}
