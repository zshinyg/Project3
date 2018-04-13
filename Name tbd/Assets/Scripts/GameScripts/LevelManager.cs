using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public class LevelManager : MonoBehaviour {

    public LevelGenerator levelGenerator;
    [HideInInspector] public GameObject mainCharacter;


	// Use this for initialization
	void Awake () {

        if (!GameObject.Find("LevelGenerator"))
        {
            Instantiate(levelGenerator);
        }
	}


    public void startLevel(int level, GameObject character)
    {
        mainCharacter = character;
        levelGenerator.SetupScene(level);
        levelGenerator.PlacePlayer(mainCharacter);

    }


    public int getNumEnemies()
    {
        return levelGenerator.getNumEnemies();
    }


    public bool isLevelOver()
    {
        if (levelGenerator.getNumEnemies() <= 0) return true;
        else return false;
    }
    // Update is called once per frame
    void FixedUpdate () {

        isLevelOver();
        getNumEnemies();
       
    }
}
