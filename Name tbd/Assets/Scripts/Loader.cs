using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameManager gameManager;          //GameManager prefab to instantiate.

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (!(GameObject.Find("GameManager")))
        //{
        //    Instantiate(gameManager);
        //}

        gameManager = GetComponent<GameManager>();
    }
}