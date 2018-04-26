using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour {

    public TestLevel testLevel;
    public GameObject mainCharacter;

    void Awake()
    {
        StartTest();
    }

    private void StartTest()
    {
        if (!(GameObject.Find("TestLevel")))
        {
            Debug.Log("Could not find TestLevel object: returning to main menu");
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }

        testLevel.BuildTestLevel();
    }
}
