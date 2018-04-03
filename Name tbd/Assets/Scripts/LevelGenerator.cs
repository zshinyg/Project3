using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed
{

	public class LevelGenerator : MonoBehaviour
	{
		public int placedFloors;
		public int numFloorTiles;									//Number of tiles that will be floor tiles.
		private enum Direction {up, right, down, left}; 				//enum used for random map generation
		public Map map;



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
			Vector3 currentPosition = map.gridPositions[Random.Range(0,map.gridPositions.Count)];	//this is our starting position
			Vector3 nextPosition;
			map.floorPositions.Add (currentPosition);
			map.gridPositions.Remove(currentPosition);											//Remove this from gridPosition
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
					map.floorPositions.Add (nextPosition);
					map.gridPositions.Remove (nextPosition);
				}
				//Successfull. Place Floor.
				currentPosition = nextPosition;
			}
		}


		//Checks if tile is a border
		bool isBorder(Vector3 nextPos){
			if (nextPos.x == 0 || nextPos.x == map.columns || nextPos.y == 0 || nextPos.y == map.rows) {
				Debug.Log ("Hit border");
				return true;
			} else {
				return false;
			}

		}


		//Checks if nextPos is a floor tile.
		bool isFloor(Vector3 nextPos){
			if(map.floorPositions.Contains(nextPos)) {			//checks if next position contains a floor
				//Debug.Log ("Contains floor");
				return true;
			} else {												//next position is clear
				//Debug.Log ("next Pos is clear");
				return false;
			}
		}
			

		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{
			int columns = 5 * level;
			int rows = 5 * level;
			//Creates the outer walls and floor.
			numFloorTiles = (columns *rows)/3;
			map.MapSetup (columns, rows);
			this.FloorSetup ();
			map.placeFloors ();
			map.placeWalls ();

		}


		void Awake(){
			if (!(GameObject.Find("Map")))
			{
				Instantiate(map);
			}
		}
	}
}