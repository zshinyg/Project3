using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using Completed;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{

    // ANTHING COMMENTED OUT FOR CHANGING TO LEVELMANAGER ITLL HAVE ***

    public static GameManager instance = null;
	public float levelStartDelay = 2f;
    public LevelManager levelManager;
	public GameObject mainCharacter;


	private Text levelText;
	private GameObject levelImage;
	private Text enemyText;
	private GameObject enemyImage;
	private int level;                                  //Current level number, expressed in game as "Day 1".
	private Transform camera;


    void Awake()
    {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

        level = 1;
		SpawnControlledPlayer ();
		InitGame ();
        
    }

	void Update(){

		ShowEnemies ();
        Debug.Log("hello");
        Debug.Log(levelManager.isLevelOver());
        if (levelManager.isLevelOver())
        {
            NextLevel();
        }
		if (mainCharacter.GetComponent<IPlayer> ().isDead ()) {
			GameOver ();
		}

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

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }


    void ShowEnemies (){
		enemyImage = GameObject.Find ("EnemyImage");
		enemyText = GameObject.Find ("EnemyText").GetComponent<Text>();
		enemyText.text = "Enemies: " + levelManager.getNumEnemies();
	}


	void InitGame(){

        /* LevelManager stuff */
        if (!GameObject.Find("LevelManager"))
        {
            Instantiate(levelManager);
        }

        levelManager.startLevel(level, mainCharacter);
        LevelTransition ();
		ShowEnemies ();
	}

    void NextLevel()
    {
        Destroy(levelManager);
        level++;
        InitGame();
    }

	void GameOver(){
		SceneManager.LoadScene ("GameOver");
	}





}