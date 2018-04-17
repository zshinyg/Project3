using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public class LevelManager : MonoBehaviour
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
        mainCharacter = character;
        levelGenerator.SetupScene(level-1);
        levelGenerator.PlacePlayer(mainCharacter);

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
            levelGenerator.ClearMap();
            return true;
        }
        else return false;
    }



    /* FixedUpdate
         * @param none
         * @return none
         * Called once every .02s, checks to see if the level is over
         */
    void FixedUpdate()
    {

        isLevelOver();
        getNumEnemies();

    }
}
