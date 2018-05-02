using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackSpawner : MonoBehaviour {

    public GameObject jack;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("TestLevel").GetComponent<TestLevel>().SpawnObject(jack);
    }
}
