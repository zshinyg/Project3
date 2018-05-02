using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItemSpawner : MonoBehaviour {

    public GameObject invincibleItem;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("TestLevel").GetComponent<TestLevel>().SpawnObject(invincibleItem);
    }
}
