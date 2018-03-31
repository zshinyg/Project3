using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

	public class LevelGenerator : MonoBehaviour
	{

		public int columns = 100;                                         //Number of columns in our game board.
		public int rows = 100;                                            //Number of rows in our game board.
		public GameObject[] floorTiles;                                 //Array of floor prefabs.
		public GameObject[] wallTiles; 									//Array of wall prefabs.
		public int myScale = 1;
		public int numFloorTiles = 50;									//Number of tiles that will be floor tiles.

		private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
		private List <Vector3> floorPositions = new List <Vector3> ();  //A list of possible tiles that are floors.
		private enum Direction {up, right, down, left}; 				//enum used for random map generation

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
				for(int y = 1; y < rows; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
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
				Debug.Log ("numFloorTiles: " + numFloorTiles);
				nextDirection = (Direction)Random.Range (0, 3);
				nextPosition = currentPosition;


				switch(nextDirection) {


				case Direction.up: 
					nextPosition.y++;
					break;

				case Direction.right:
					nextPosition.x++;
					break;

				case Direction.down:
					nextPosition.y--;
					break;

				case Direction.left:
					nextPosition.x--;
					break;

				}

				if (CheckNextPos(nextPosition)) {
					currentPosition = nextPosition;
					floorPositions.Add (currentPosition);
					gridPositions.Remove(currentPosition);
				} else {
					numFloorTiles++;
				}

	
			}

		}


		//Checks if the next position is a valid tile spot
		bool CheckNextPos(Vector3 nextPos) {

			if (nextPos.x == 0 || nextPos.x == columns || nextPos.y == 0 || nextPos.y == rows) {
				return false;
			} else {
				return true;
			}

		}


		void wallSetup() {

			GameObject toInstantiate = wallTiles[Random.Range (0,floorTiles.Length)];
			for(int i = 0; i < gridPositions.Count; i++){
				GameObject instance = Instantiate (toInstantiate, gridPositions[i], Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);

			}
		}


		//Sets up the outer walls and floor (background) of the game board.
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
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(x == 0 || x == columns || y == 0 || y == rows)
						toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];


					boardHolder.localScale = new Vector3 (myScale, myScale, myScale);
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}


	
		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{

			//Creates the outer walls and floor.
			this.BoardSetup();

			//Reset our list of gridpositions.
			this.InitialiseList();

			this.FloorSetup ();

			this.wallSetup ();

			Debug.Log ("FloorPos: "+floorPositions.Count);
			Debug.Log ("GirdPos: "+gridPositions.Count);

		}
	}
}