using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using Completed;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour , IGameEventSystem

{

    // ANTHING COMMENTED OUT FOR CHANGING TO LEVELMANAGER ITLL HAVE ***

    public static GameManager instance = null;
    public float levelStartDelay = 2f;
    public LevelManager levelManager;
    public GameObject mainCharacter;
    public GameObject[] mainCharacters;


    private Text levelText;
    private GameObject levelImage;
    private Text enemyText;
    private GameObject enemyImage;
    private int level = 0;                                  //Current level number, expressed in game as "Day 1".



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

        DiscernMainCharacter();
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(mainCharacter);
        SpawnControlledPlayer();

    }


    /* DiscernMainCharacter
     * @param none
     * @return none
     * Determines which GameObject in the array to use based on the value of CharacterName
     */
    private void DiscernMainCharacter()
    {
        if (GameVariables.CharacterName == "Gingy")   mainCharacter = mainCharacters[0];
        
        else if (GameVariables.CharacterName == "ShadowGingy")   mainCharacter = mainCharacters[1];

       
    }

    /* Update
         * @param none
         * @return none
         * Called based on the clock time
         * Updates UI
         */
    void Update()
    {
        ShowEnemies();
    }



    /* SpawnControlledPlayer
         * @param none
         * @return none
         * Creates the GameObject of mainCharacer
         */
    private void SpawnControlledPlayer()
    {
        Debug.Log(mainCharacter);
        mainCharacter = Instantiate(mainCharacter) as GameObject;
    }


    /* LevelTransition
         * @param none
         * @return none
         * Shows and hides the level screen in between levels
         */
    void LevelTransition()
    {
        levelImage = GameObject.Find("LevelImage");
        levelImage.SetActive(true);
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;

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

        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        LevelTransition();
        levelManager.startLevel(level, mainCharacter);
        ShowEnemies();
    }


    /* NextLevel
         * @param none
         * @return none
         * Increments level
         */
    public void LevelOver()
    {
        //Debug.Log("Inside LevelOver of GameManager");
        SceneManager.LoadScene(1);
    }


    /* GameOver
         * @param none
         * @return none
         * Loads teh game over scene
         */
    public void GameOver()
    {
        GameVariables.LevelReached = level;
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    /* OnEnable
     * @param none
     * @return none
     * Called by the Unity event system when the scene is enabled to start level
     */
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    /* OnDisable
     * @param none
     * @return none
     * Called by the unity event system when the scene is disabled to iterate to next level
     */
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }



    /* OnLevelFinishedLoading
     * @param Scene, LoadSceneMode
     * @return none
     * When the level is enabled/loaded, this scene method is called and iterates to the next level
     */
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        level++;
        InitGame();
    }

}