using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinManSpawner : MonoBehaviour {

    public GameObject muffinMan;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("TestLevel").GetComponent<TestLevel>().SpawnObject(muffinMan);
    }
}
