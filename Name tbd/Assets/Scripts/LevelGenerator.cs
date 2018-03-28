using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {

	public int columns = 10;
	public int rows = 10;

	public setSize(int col, int row){
		columns = col;
		rows = row;
	}

	public GameObject[] floorTiles;
	public GameObject[] wallTiles;

	private Transform boardHolder;

	private List<Vector3> gridPositions = new List<Vector3>();

	void initializeList(){

		gridPositions.Clear();

		for (int x = 1; x < columns; x++){

			for (int y = 1; y < rows; y++){

				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	} 

	void boardSetup(){

		boardHolder = new GameObject("Board").transform;

		for (int x = -1; x < columns+1; x ++ ){
			for (int y = -1; y < rows+1; y++){

				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
				if ((x == -1) || (x == columns) || (y == -1) || (y == rows)){

					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];

					GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quanternion.identity) as GameObject;

					instance.transfrom.SetParent(boardHolder);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}

}
