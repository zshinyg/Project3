using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour {


    private int numRows = 30;
    private int numCols = 30;
    private Transform boardHolder;

    public GameObject[] wallTiles;
    public GameObject[] floorTiles;


    /* BuildTestLevel
     * @param none
     * @return none
     * In Charge building and setting up the test level
     */
	public void BuildTestLevel()
    {
        FloorSetup();
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
}
