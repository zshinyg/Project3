using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using Completed;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	public float levelStartDelay = 2f;
	public LevelGenerator levelGenerator;    //Store a reference to our BoardManager which will set up the level.
	public GameObject mainCharacter;


	private Text levelText;
	private GameObject levelImage;
	private Text enemyText;
	private GameObject enemyImage;
	private int level = 1;                                  //Current level number, expressed in game as "Day 1".
	private bool doingSetup;
	private Transform camera;


    void Awake()
    {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
			

        if (!(GameObject.Find("LevelGenerator")))
        {
            Instantiate(levelGenerator);
        }
		SpawnControlledPlayer ();
		InitGame ();
        levelGenerator.SetupScene(level);

    }

	void FixedUpdate(){

		ShowEnemies ();
		//Debug.Log ("Number of Enemies inside GM: " + levelGenerator.getNumEnemies());
		if (levelGenerator.getNumEnemies() <= 0) {
			Debug.Log ("Level Over");
			//LevelOver ();
		}
		//Debug.Log(mainCharacter.GetComponent<IPlayer> ().isDead ());
		if (mainCharacter.GetComponent<IPlayer> ().isDead ()) {
			GameOver ();
		}

	}

	private void HideLevelImage() {
		levelImage.SetActive (false);	
		doingSetup = false;
	}

	private void Spawn(){
		camera.parent = Camera.main.transform;
	}

	private void SpawnControlledPlayer() {
		mainCharacter = (GameObject)Instantiate (mainCharacter) as GameObject;
	}

	void LevelTransition(){
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text>();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);
	}
		
	void ShowEnemies (){
		enemyImage = GameObject.Find ("EnemyImage");
		enemyText = GameObject.Find ("EnemyText").GetComponent<Text>();
		enemyText.text = "Enemies: " + levelGenerator.getNumEnemies();
	}

	void InitGame(){
		doingSetup = true;
		LevelTransition ();
		ShowEnemies ();
		levelGenerator.PlacePlayer (mainCharacter);
	}

	private void OnLevelWasLoaded(int index){
		InitGame ();
		level++;
	}

	void GameOver(){
		SceneManager.LoadScene ("GameOver");
	}

//	void LevelOver(){
//		Destroy (levelGenerator);
//		Instantiate(levelGenerator);
//		SpawnControlledPlayer ();
//		InitGame ();
//		levelGenerator.SetupScene(level);
//	}



}