using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IEventSystemHandler
{

    public LevelGenerator levelGenerator;
    [HideInInspector] public GameObject mainCharacter;



    /* Awake
     * @param none
     * @return none
     * Called to initialize LevelManager and create LevelGenerator
     */
    void Awake()
    {
        if (!GameObject.Find("LevelGenerator"))
        {
            Instantiate(levelGenerator);
        }
       

    }

    /* startLevel
         * @param int level, GameObject character
         * @return none
         * Starts the level by placing player and setting up scene
         */
    public void startLevel(int level, GameObject character)
    {
        GameVariables.LevelDuration = 0;
        mainCharacter = character;
        levelGenerator.SetupScene(level, mainCharacter);
    
    }

    /* getNumEnemies
         * @param none
         * @return int
         * returns the number of enemies from levelGenerator
         */
    public int getNumEnemies()
    {
        return levelGenerator.getNumEnemies();
    }



    /* isLevelOver
         * @param none
         * @return bool
         * Checks to see fi the number of enemies is 0
         * true if there are no enemies, false otherwise
         */
    public bool isLevelOver()
    {


        if (levelGenerator.getNumEnemies() <= 0)
        {
            return true;
        }
        else return false;
    }


    void NextLevel()
    {
        GameObject target = GameObject.Find("GameManager");
        //Debug.Log("Inside NextLevel of LevelManager");
        ExecuteEvents.Execute<IGameEventSystem>(GameObject.Find("GameManager"), null, (x, y) => x.LevelOver());

    }
    /* FixedUpdate
         * @param none
         * @return none
         * Called once every .02s, checks to see if the level is over
         */
    void FixedUpdate()
    {
        //Debug.Log("isLoading is: " + isLoading);
        if (SceneManager.GetActiveScene().isLoaded)
        {
            if (isLevelOver())
            {
                levelGenerator.ClearMap();
                Invoke("NextLevel", 1);
            }
        }
        GameVariables.LevelDuration += .02;
        
    }


}
