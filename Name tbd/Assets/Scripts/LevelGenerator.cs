using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed
{

	public class LevelGenerator : MonoBehaviour
	{

		public int columns;                                       //Number of columns in our game board.
		public int rows;                                            //Number of rows in our game board.
		public GameObject[] floorTiles;                                 //Array of floor prefabs.
		public GameObject[] wallTiles; 									//Array of wall prefabs.
		public int numFloorTiles = 5;									//Number of tiles that will be floor tiles.
		private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
		private List <Vector3> floorPositions = new List <Vector3> ();  //A list of possible tiles that are floors.
		private enum Direction {up, right, down, left}; 				//enum used for random map generation
		public Map map;

		//Clears our list gridPositions and prepares it to generate a new board.
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
			//Debug.Log ("GridPosCount: "+gridPositions.Count);
			//Debug.Log ("FloorPosCount: "+floorPositions.Count);
		}


		Vector3 moveNextPos(Vector3 nextPos, Direction nextDir){
			switch (nextDir) {

			case Direction.up: 
				nextPos.y++;
				break;

			case Direction.right:
				nextPos.x++;
				break;

			case Direction.down:
				nextPos.y--;
				break;

			case Direction.left:
				nextPos.x--;
				break;
			}
			return nextPos;
		}



		//Sets up the floor tiles randomly
		void FloorSetup() {

			//Vector3 that chooses random position to start on map
			Vector3 currentPosition = gridPositions[Random.Range(0,gridPositions.Count)];	//this is our starting position
			Vector3 nextPosition;
			floorPositions.Add (currentPosition);
			gridPositions.Remove(currentPosition);											//Remove this from gridPosition
			Direction nextDirection;

			for (int i = 0; i < numFloorTiles; i++) {
				nextDirection = (Direction)Random.Range (0, 3);								//Select a random direction
				nextPosition = moveNextPos (currentPosition, nextDirection);					//set the next position
				Debug.Log("Before while loop");
				while (isBorder (nextPosition)) {
					Debug.Log ("inside While");
					if (nextDirection == Direction.left) {
						nextDirection = Direction.up;
					} else {
						nextDirection++;
					}
					nextPosition = moveNextPos(currentPosition,nextDirection);
				}
				if (isFloor (nextPosition)) {
					Debug.Log("in if");
					numFloorTiles++;
				} else {
					Debug.Log("in ");
					floorPositions.Add (nextPosition);
					gridPositions.Remove (nextPosition);
				}
				//Successfull. Place Floor.
				currentPosition = nextPosition;
			}
		}


		//Checks if tile is a border
		bool isBorder(Vector3 nextPos){
			if (nextPos.x == 0 || nextPos.x == columns || nextPos.y == 0 || nextPos.y == rows) {
				Debug.Log ("Hit border");
				return true;
			} else {
				return false;
			}

		}


		//Checks if nextPos is a floor tile.
		bool isFloor(Vector3 nextPos){
			if(floorPositions.Contains(nextPos)) {			//checks if next position contains a floor
				//Debug.Log ("Contains floor");
				return true;
			} else {												//next position is clear
				//Debug.Log ("next Pos is clear");
				return false;
			}
		}
			

		//Place the foor tiles
		void placeFloors(){
			GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
			for (int i = 0; i < floorPositions.Count; i++) {
				GameObject instance = Instantiate (toInstantiate, floorPositions [i], Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}

		//Place the wall tiles
		void placeWalls() {
			GameObject toInstantiate = wallTiles[Random.Range (0,wallTiles.Length)];
			for(int i = 0; i < gridPositions.Count; i++){
				GameObject instance = Instantiate (toInstantiate, gridPositions[i], Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}


		//Sets up the outer walls 
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


		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{
			//Creates the outer walls and floor.
			numFloorTiles = 100;
			this.BoardSetup();
			//Reset our list of gridpositions.
			this.InitialiseList();
			this.FloorSetup ();
			this.placeFloors ();
			this.placeWalls ();
			Debug.Log ("FloorPos: "+floorPositions.Count);
			Debug.Log ("GridPos: "+gridPositions.Count);
		}


		void Awake(){
			if (!(GameObject.Find("Map")))
			{
				Instantiate(map);
			}
		}
	}
}