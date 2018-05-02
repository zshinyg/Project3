﻿using UnityEngine;
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
		public int numItems;
		public GameObject[] Items;
		public Vector3 playerPos;
		public GameObject Enemy;
	
		public int numEnemies;


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



		void FixedUpdate(){
			//GameObject[] EnemyArr = GameObject.FindGameObjectsWithTag ("Enemy");
			//UpdateNumEnemies (EnemyArr.Length);
			//Debug.Log ("Number of enemies inside LG: " + numEnemies);
			
		}


		//Sets up the floor tiles randomly
		void FloorSetup() {

			//Vector3 that chooses random position to start on map
			Vector3 currentPosition = map.gridPositions[Random.Range(0,map.gridPositions.Count)];	//this is our starting position
			Vector3 nextPosition;
			map.floorPositions.Add (currentPosition);
			map.gridPositions.Remove(currentPosition);											//Remove this from gridPosition
			Direction nextDirection;

			//Debug.Log (currentPosition.x +", " + currentPosition.y);

			for (int i = 0; i < numFloorTiles; i++) {
				nextDirection = (Direction)Random.Range (0, 3);								//Select a random direction
				nextPosition = moveNextPos (currentPosition, nextDirection);					//set the next position
				while (isBorder (nextPosition)) {
					if (nextDirection == Direction.up) {
						nextDirection = Direction.left;
					} else {
						nextDirection--;
					}
					nextPosition = moveNextPos(currentPosition,nextDirection);
				}
				if (isFloor (nextPosition)) {
					numFloorTiles++;
				} else {

					map.floorPositions.Add (nextPosition);
					map.gridPositions.Remove (nextPosition);
				}
				//Successfull. Place Floor.
				currentPosition = nextPosition;
			}
		}



		void generateEnemies(int numberOfEnemies){
			Vector3 enemyPos;
			GameObject toInstantiate;

			for (int i = 0; i < numberOfEnemies; i++) {
				toInstantiate = Enemy;
				enemyPos = map.floorPositions [Random.Range (0, map.floorPositions.Count)];
				GameObject instance = Instantiate (toInstantiate, enemyPos, Quaternion.identity) as GameObject;
				instance.transform.SetParent (map.GetBoardHolder());
				map.gridPositions.Remove (enemyPos);
			}		
		}
		

		void generateItems(int level){
			Vector3 itemPos;
			GameObject toInstantiate;

			numItems = (int)Mathf.Log (level, 2);
			for (int i = 0; i < numItems; i++) {
				toInstantiate =Items[Random.Range (0,Items.Length)];
				itemPos = map.floorPositions [Random.Range (0, map.floorPositions.Count)];
				GameObject instance = Instantiate (toInstantiate, itemPos, Quaternion.identity) as GameObject;
				instance.transform.SetParent (map.GetBoardHolder());
			}
		}


		//Checks if tile is a border
		bool isBorder(Vector3 nextPos){
			if (nextPos.x == 0 || nextPos.x == map.columns || nextPos.y == 0 || nextPos.y == map.rows) {
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
			int columns = (int)(0.5 * level) + 5;
			int rows = (int)(0.5 * level) +5;
			if (level >= 50) {
				columns = 30;
				rows = 30;
			}
			//numEnemies = level;
			//Creates the outer walls and floor.
			numFloorTiles = (columns *rows)/3;
			map.MapSetup (columns, rows);
			this.FloorSetup ();
			map.placeFloors ();
			map.placeWalls ();
			generateItems (level);
			generateEnemies (level);
		}

		public void PlacePlayer(GameObject Player){
			playerPos = map.floorPositions [Random.Range (0, map.floorPositions.Count)];
			Player.transform.position = playerPos;
			map.gridPositions.Remove (playerPos);
		}

		void Awake(){
			if (!(GameObject.Find("Map")))
			{
				Instantiate(map);
			}
		}

		public int getNumEnemies(){
			//Debug.Log (numEnemies);
			numEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			//Debug.Log("Number inside get: " + numEnemies);
			return numEnemies;
		}
	}
}