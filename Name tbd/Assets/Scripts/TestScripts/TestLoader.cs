using UnityEngine;
using System.Collections;


public class TestLoader : MonoBehaviour
{
    public TestManager testManager;          //TestManager prefab to instantiate.

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (!(GameObject.Find("GameManager")))
        //{
        //    Instantiate(gameManager);
        //}

        testManager = GetComponent<TestManager>();
    }
}