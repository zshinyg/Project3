using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed
{

    public class LevelGenerator : MonoBehaviour
    {
        public int placedFloors;
        public int numFloorTiles;                                   //Number of tiles that will be floor tiles.
        private enum Direction { up, right, down, left };               //enum used for random map generation
        public Map map;
        public int numItems;
        public GameObject[] Items;
        public Vector3 playerPos;
        public GameObject Enemy1;
        public GameObject Enemy2;
        public List<Vector3> enemyPositions;

        public int numEnemies;
        public int myLevel;


        /* moveNextPos
         * @param Vector3 nextPos, Direction nextDir (enum)
         * @return Vector3
         * In order to generate the map randomly, this is used to methodically move the next position in a clockwise direction
         */
        Vector3 moveNextPos(Vector3 nextPos, Direction nextDir)
        {
            switch (nextDir)
            {

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


        /* isBorder
        * @param Vector3 nextPos
        * @return bool
        * checks to see if the next position to be placed is a border tile
        * returns true if it is, false if it is not
        */
        bool isBorder(Vector3 nextPos)
        {
            if (nextPos.x == 0 || nextPos.x == map.columns || nextPos.y == 0 || nextPos.y == map.rows)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /* isFloor
         * @param Vector3 nextPos
         * @return bool
         * Checks to see if the next position is a floor tile that has already been placed
         * Returns true if it is already a floor, false otherwise
         */
        bool isFloor(Vector3 nextPos)
        {
            if (map.floorPositions.Contains(nextPos))
            {           //checks if next position contains a floor
                        //Debug.Log ("Contains floor");
                return true;
            }
            else
            {                                               //next position is clear
                                                            //Debug.Log ("next Pos is clear");
                return false;
            }
        }


        /* SetupScene
         * @param int level
         * @return none
         * Main function of LevelGenerator
         * Responsible for calling all other functions that setup the map
         */
        public void SetupScene(int level, GameObject Player)
        {
            myLevel = level;

            int columns = (int)(0.5 * level) + 5;
            int rows = (int)(0.5 * level) + 5;
            if (level >= 50)
            {
                columns = 30;
                rows = 30;
            }
            //numEnemies = level;
            //Creates the outer walls and floor.
            numFloorTiles = (columns * rows) / 3;
            map.MapSetup(columns, rows);
            this.FloorSetup();
            map.placeFloors();
            map.placeWalls();
            generateItems(level);
            generateEnemies(level);
            PlacePlayer(Player);
        }



        /* FloorSetup
         * @param none
         * @return none
         * Sets up the internal walls and floors randomly
         */
        void FloorSetup()
        {

            //Vector3 that chooses random position to start on map
            Vector3 currentPosition = map.gridPositions[Random.Range(0, map.gridPositions.Count)];  //this is our starting position
            Vector3 nextPosition;
            map.floorPositions.Add(currentPosition);
            map.gridPositions.Remove(currentPosition);                                          //Remove this from gridPosition
            Direction nextDirection;

            //Debug.Log (currentPosition.x +", " + currentPosition.y);

            for (int i = 0; i < numFloorTiles; i++)
            {
                nextDirection = (Direction)Random.Range(0, 3);                              //Select a random direction
                nextPosition = moveNextPos(currentPosition, nextDirection);                 //set the next position
                while (isBorder(nextPosition))
                {
                    if (nextDirection == Direction.up)
                    {
                        nextDirection = Direction.left;
                    }
                    else
                    {
                        nextDirection--;
                    }
                    nextPosition = moveNextPos(currentPosition, nextDirection);
                }
                if (isFloor(nextPosition))
                {
                    numFloorTiles++;
                }
                else
                {

                    map.floorPositions.Add(nextPosition);
                    map.gridPositions.Remove(nextPosition);
                }
                //Successfull. Place Floor.
                currentPosition = nextPosition;
            }
        }


        /* generateEnemies
         * @param int numberOfEnemies
         * @return none
         * Generates gameobjects of enemies and places them randomly
         */
        void generateEnemies(int numberOfEnemies)
        {
            Vector3 enemyPos;
            GameObject toInstantiate;
            if (myLevel % 5 == 0)
            {
                for (int i = 0; i < myLevel/5; i++)
                {
                    toInstantiate = Enemy2;
                    enemyPos = map.floorPositions[Random.Range(0, map.floorPositions.Count)];
                    GameObject instance = Instantiate(toInstantiate, enemyPos, Quaternion.identity) as GameObject;
                    instance.transform.SetParent(map.GetBoardHolder());
                    map.gridPositions.Remove(enemyPos);
                    enemyPositions.Add(enemyPos);
                }
            }
            else
            {
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    toInstantiate = Enemy1;
                    enemyPos = map.floorPositions[Random.Range(0, map.floorPositions.Count)];
                    GameObject instance = Instantiate(toInstantiate, enemyPos, Quaternion.identity) as GameObject;
                    instance.transform.SetParent(map.GetBoardHolder());
                    map.gridPositions.Remove(enemyPos);
                    enemyPositions.Add(enemyPos);
                }
            }
        }

        /* generateItems
         * @param int level
         * @return none
         * creates the gameobjects for items and places them randomly
         */
        void generateItems(int level)
        {
            Vector3 itemPos;
            GameObject toInstantiate;

            numItems = (int)Mathf.Log(level, 2);
            for (int i = 0; i < numItems; i++)
            {
                toInstantiate = Items[Random.Range(0, Items.Length)];
                itemPos = map.floorPositions[Random.Range(0, map.floorPositions.Count)];
                GameObject instance = Instantiate(toInstantiate, itemPos, Quaternion.identity) as GameObject;
                instance.transform.SetParent(map.GetBoardHolder());
            }
        }

        /* PlacePlayer
         * @param GameObject Player
         * @return none
         * Places the player randomly in the map outside of the range of enemies
         */
        void PlacePlayer(GameObject Player)
        {

            bool tooClose;
            Vector3 delta;
            playerPos = pickPlayerPostion();

            // Make sure that the player isn't placed too close to the enemy
            //do
            //{
            //    tooClose = false;
            //    foreach (Vector3 position in enemyPositions)
            //    {
            //        delta = position - playerPos;
            //        Debug.Log("Value of Delta: " + Math.Abs(delta.magnitude));
            //        if (Math.Abs(delta.magnitude) < 2)
            //        {
            //            tooClose = true;
            //            playerPos = pickPlayerPostion();
            //        }
            //        else break;
            //    }
            //} while (tooClose);

            Player.transform.position = playerPos;
        }

        /* pickPlayerPosition
         * @param none
         * @return Vector3
         * Picks a random gridposition to place the player
         */
        Vector3 pickPlayerPostion()
        {
           // Debug.Log("Size of floor positions: " + map.floorPositions.Count);
            Vector3 pos = map.floorPositions[Random.Range(0, map.floorPositions.Count-1)];
            map.floorPositions.Remove(pos);
            return pos;

        }


        /* Awake
         * @param none
         * @return none
         * Called when LevelGenerator is created and is responsible for starting the map generation
         */
        void Awake()
        {
            if (!(GameObject.Find("Map")))
            {
                Instantiate(map);
            }
        }


        /* getNumEnemies
         * @param none
         * @return int
         * returns the number of enemies remaining
         */
        public int getNumEnemies()
        {
            int returnNum = 0;
            //Debug.Log (numEnemies);
            GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject deadEnemy in enemiesLeft) {
                if (!(deadEnemy.GetComponent<IEnemy>().isDead()))
                {
                    returnNum++;
                }
            }

            //Debug.Log("Number inside get: " + numEnemies);
            return returnNum;
        }


        public void ClearMap()
        {
            map.gridPositions.Clear();
            map.floorPositions.Clear();
            enemyPositions.Clear();
          
        }
    }
}