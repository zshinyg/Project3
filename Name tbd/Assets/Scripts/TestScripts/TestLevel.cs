using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour {


    private int numRows = 15;
    private int numCols = 15;
    private Transform boardHolder;
    private Vector3 muffinManSpawnerPos;
    private Vector3 jackSpawnerPos;
    private Vector3 healthItemSpawnerPos;
    private Vector3 invincibleItemSpawnerPos;
    private Vector3 attackItemSpawnerPos;

    public GameObject[] wallTiles;
    public GameObject[] floorTiles;

    public GameObject muffinManSpawner;
    public GameObject jackSpawner;
    public GameObject healthItemSpawner;
    public GameObject invincibleItemSpawner;
    public GameObject attackItemSpawner;

    /* BuildTestLevel
     * @param none
     * @return none
     * In Charge building and setting up the test level
     */
	public void BuildTestLevel()
    {
        FloorSetup();
        PlaceSpawners();
    }



    /* FloorSetup
     * @param none
     * @return none
     * Builds a 30x30 map for testing
     */
    private void FloorSetup()
    {
        for (int i = 0; i <= numCols; i++)
        {
            for (int j = 0; j <= numRows; j++)
            {
                GameObject toInstantiate;
                if ((i == 0) || (j == 0) || (i == numCols) || (j == numRows))
                {
                    toInstantiate = wallTiles[Random.Range(0, wallTiles.Length - 1)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                else
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length - 1)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }


    /* PlacePlayer
     * @param GameObject mainCharacter
     * @return none
     * Places the mainCharacter in the bottom left hand corner
     */
     public void PlacePlayer(GameObject mainCharacter)
    {
        mainCharacter.transform.SetParent(boardHolder);
        mainCharacter.transform.position = new Vector3(1f, 1f, 0f);
    }


    /*
     * 
     * 
     * 
     */
     public void PlaceSpawners()
    {
        muffinManSpawnerPos = new Vector3(numCols / 3, numRows / 3, 0f);
        jackSpawnerPos = new Vector3(2 * numCols / 3, numRows / 3, 0f);
        healthItemSpawnerPos = new Vector3(2 * numCols / 3, 2 * numRows / 3, 0f);
        invincibleItemSpawnerPos = new Vector3(numCols / 3, 2 * numRows / 3, 0f);
        attackItemSpawnerPos = new Vector3(numCols / 2, numRows / 2, 0f);


        muffinManSpawner = Instantiate(muffinManSpawner, muffinManSpawnerPos, Quaternion.identity);
        jackSpawner = Instantiate(jackSpawner, jackSpawnerPos, Quaternion.identity);
        healthItemSpawner = Instantiate(healthItemSpawner, healthItemSpawnerPos, Quaternion.identity);
        invincibleItemSpawner = Instantiate(invincibleItemSpawner, invincibleItemSpawnerPos, Quaternion.identity);
        attackItemSpawner = Instantiate(attackItemSpawner, attackItemSpawnerPos, Quaternion.identity);

    }
}
