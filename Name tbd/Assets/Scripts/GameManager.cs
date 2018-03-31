using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using Completed;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;              //Static instance of GameManager which allows it to be accessed by any other script.
	public LevelGenerator levelGenerator;                       //Store a reference to our BoardManager which will set up the level.
	private int level = 1;                                  //Current level number, expressed in game as "Day 1".
    
    void Awake()
    {

        if (!(GameObject.Find("LevelGenerator")))
        {
            Instantiate(levelGenerator);
        }
        //levelGenerator = gameObject.GetComponent<LevelGenerator>();
        
        levelGenerator.SetupScene(level);

    }
}