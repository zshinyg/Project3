using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemSpawner : MonoBehaviour {

    public GameObject attackItem;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("TestLevel").GetComponent<TestLevel>().SpawnObject(attackItem);
    }
}
