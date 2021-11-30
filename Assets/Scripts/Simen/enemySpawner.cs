using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] objects;  // The prefab to be spawned.
    public float spawnTime = 10f;  // How long between each spawn.
    private Vector3 _spawnPosition;

    // Use this for initialization
    private void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        _spawnPosition.x = Random.Range(-17, 17);
        _spawnPosition.y = 0.5f;
        _spawnPosition.z = Random.Range(-17, 17);
        
        Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)], _spawnPosition, quaternion.identity);
    }
}
