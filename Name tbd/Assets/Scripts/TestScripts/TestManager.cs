using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour, ITestEventSystem {

    public TestLevel testLevel;
    public GameObject mainCharacter;


    /* Awake
     * @param none
     * @return none
     * Called when the game object is called
     */
    void Awake()
    {
        BeginTest();
    }


    /* StartTest
     * @param none
     * @return none
     * Sets up board, spawns player, and starts the test level
     */
    private void BeginTest()
    {
        if (!(GameObject.Find("TestLevel")))
        {
            Debug.Log("Could not find TestLevel object: returning to main menu");
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }

        testLevel.BuildTestLevel();
        SpawnTestPlayer();
    }

    /* SpawnTestPlayer
     * @param none
     * @return none
     * Spawns the controlled player for testing
     */
    private void SpawnTestPlayer()
    {
        mainCharacter = Instantiate(mainCharacter) as GameObject;
        testLevel.PlacePlayer(mainCharacter);
    }

    public void EndTest()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*
     * Null Method used to fulfill ITestEventSystem
     * 
     */
    public void StartTest() { }
}
