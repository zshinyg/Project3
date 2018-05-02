using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemSpawner : MonoBehaviour {

    public GameObject healthItem;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("TestLevel").GetComponent<TestLevel>().SpawnObject(healthItem);
    }
}
