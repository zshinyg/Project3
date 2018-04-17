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
    private bool isLoading;



    /* Awake
         * @param none
         * @return none
         * Called when GameManager is initialized
         * Starts the entire game
         */
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        isLoading = true;
        level = 1;
        SpawnControlledPlayer();
        InitGame();

    }


    /* Update
         * @param none
         * @return none
         * Called based on the clock time
         * Checks to see if the level is over or if the game is over
         */
    void Update()
    {

        ShowEnemies();
        Debug.Log(levelManager.isLevelOver());
        if (levelManager.isLevelOver())
        {
            NextLevel();
            isLoading = false;
        }
        if (mainCharacter.GetComponent<IPlayer>().isDead())
        {
            GameOver();
        }

    }

    /* Spawn
         * @param none
         * @return none
         * Places the camera of the player on the main camera
         */
    private void Spawn()
    {
        camera.parent = Camera.main.transform;
    }

    /* SpawnControlledPlayer
         * @param none
         * @return none
         * Creates the GameObject of mainCharacer
         */
    private void SpawnControlledPlayer()
    {
        mainCharacter = (GameObject)Instantiate(mainCharacter) as GameObject;
    }


    /* LevelTransition
         * @param none
         * @return none
         * Shows and hides the level screen in between levels
         */
    void LevelTransition()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }


    /* HideLevelImage
         * @param none
         * @return none
         * Hides the level transition screen
         */
    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }



    /* ShowEnemeis
         * @param none
         * @return none
         * UI element that shows the number of enemies remaining
         */
    void ShowEnemies()
    {
        enemyImage = GameObject.Find("EnemyImage");
        enemyText = GameObject.Find("EnemyText").GetComponent<Text>();
        enemyText.text = "Enemies: " + levelManager.getNumEnemies();
    }

    /* InitGame
         * @param none
         * @return none
         * Initializes the LevelManager and starts each level
         */
    void InitGame()
    {

        /* LevelManager stuff */
        if (!GameObject.Find("LevelManager"))
        {
            Instantiate(levelManager);
        }

        LevelTransition();
        levelManager.startLevel(level, mainCharacter);
        ShowEnemies();
        isLoading = false;
    }


    /* NextLevel
         * @param none
         * @return none
         * Increments level
         */
    void NextLevel()
    {
        level++;
        if (!isLoading)
        {
            SceneManager.LoadScene("Level");
        }
        InitGame();
    }


    /* GameOver
         * @param none
         * @return none
         * Loads teh game over scene
         */
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }





}