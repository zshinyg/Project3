using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public GameObject[] floorTiles;                                 //Array of floor prefabs.
	public GameObject[] wallTiles; 									//Array of wall prefabs.
	public List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
	public List <Vector3> floorPositions = new List <Vector3> ();  //A list of possible tiles that are floors.
	public int columns;                                       //Number of columns in our game board.
	public int rows; 


	private Transform boardHolder;   								//A variable to store a reference to the transform of our Board object.

	/**
	 * Returns a holder in the hiearchy to hold walls, floor etc...
	 * @Param None
	 * @Return Transform 
	**/
	public Transform GetBoardHolder(){
		return boardHolder;
	}


	/**
	 * Clears our list gridPositions and prepares it to generate a new board.
	 * @Param None
	 * @Return None
	**/
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();
		floorPositions.Clear ();

		//Loop through x axis (columns).
		for(int x = 1; x < columns; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 1; y < rows ; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}

	/**
	 * Place the foor tiles
	 * @Param None
	 * @Return None
	**/
	public void placeFloors(){
		GameObject toInstantiate;
		for (int i = 0; i < floorPositions.Count; i++) {
			toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
			GameObject instance = Instantiate (toInstantiate, floorPositions [i], Quaternion.identity) as GameObject;
			instance.transform.SetParent (boardHolder);
		}
	}

	/**
	 * Place the wall tiles
	 * @Param None
	 * @Return None
	**/
	public void placeWalls() {
		GameObject toInstantiate;
		for(int i = 0; i < gridPositions.Count; i++){
			toInstantiate = wallTiles[Random.Range (0,wallTiles.Length)];
			GameObject instance = Instantiate (toInstantiate, gridPositions[i], Quaternion.identity) as GameObject;
			instance.transform.SetParent (boardHolder);
		}
	}

	/**
	 * Sets up outer walls
	 * @Param None
	 * @Return None
	**/
	void BoardSetup()
	{


		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;

		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(int x = 0; x < columns + 1; x++)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(int y = 0; y < rows + 1; y++)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate;


				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if (x == 0 || x == columns || y == 0 || y == rows) {
					toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}
	}


	/**
	 * sets the size of the map and calls BoardSetup()
	 * @Param int cols
	 * @Param int row
	 * @Return None
	**/
	public void MapSetup(int cols, int row){
		columns = cols;
		rows = row;

		BoardSetup();
		//Reset our list of gridpositions.
		InitialiseList();

	}

}
