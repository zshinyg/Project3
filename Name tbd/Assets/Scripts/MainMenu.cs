using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	/**
	 * Loads the scene to play the game
	 * @Param None
	 * @Return None
	**/
	public void PlayGame(){
		SceneManager.LoadScene ("Level");
	}

	/**
	 * Quits the application
	 * @Param None
	 * @Return None
	**/
	public void QuitGame(){
		Application.Quit ();
	}
	
}
