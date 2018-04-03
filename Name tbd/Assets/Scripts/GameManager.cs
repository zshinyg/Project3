using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using Completed;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public float levelStartDelay = 2f;
	public static GameManager instance;              //Static instance of GameManager which allows it to be accessed by any other script.
	public LevelGenerator levelGenerator;    //Store a reference to our BoardManager which will set up the level.


	private Text levelText;
	private GameObject levelImage;
	private int level = 2;                                  //Current level number, expressed in game as "Day 1".
	private bool doingSetup;



    void Awake()
    {

        if (!(GameObject.Find("LevelGenerator")))
        {
            Instantiate(levelGenerator);
        }

		InitGame ();
        levelGenerator.SetupScene(level);

    }


	private void HideLevelImage() {
		levelImage.SetActive (false);	
		doingSetup = false;
	}


	void InitGame(){
		doingSetup = true;

		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text>();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);
	}

	private void OnLevelWasLoaded(int index){
		level++;
		InitGame ();
	}

}