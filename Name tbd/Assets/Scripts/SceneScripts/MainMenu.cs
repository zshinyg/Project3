using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Awake()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        Destroy(music);
    }
	/**
	 * Loads the scene to play the game
	 * @Param None
	 * @Return None
	**/
	public void PlayGame(){
		SceneManager.LoadScene ("InputName");
	}

	/**
	 * Quits the application
	 * @Param None
	 * @Return None
	**/
	public void QuitGame(){
		Application.Quit ();
	}

    /* TestMode
     * @param none
     * @return none
     * Allows the user to go into test mode of the game
     */
     public void TestMode()
    {
        SceneManager.LoadScene("TestMode");
    }
	
}
