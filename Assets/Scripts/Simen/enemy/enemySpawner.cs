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
    private Vector3 _spawnPosition; // Where they will spawn
    [Range(0,6)] public int spawnAmount;// the amount of enemy spawning
    public float spawnOffset; // Distance of where they spawn from spawner

    private void OnTriggerEnter(Collider other)
    {
        print("Boom, I collided");
        
        if (other.gameObject.CompareTag("Player"))
        {
            _spawnPosition.x = Random.Range(transform.position.x-spawnOffset, transform.position.x+spawnOffset);
            _spawnPosition.y = 0.5f;
            _spawnPosition.z = Random.Range(transform.position.z-spawnOffset, transform.position.z+spawnOffset);

            for (int i = 0; i < spawnAmount; i++)
            {
                Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)], _spawnPosition, quaternion.identity);
            }
           
        }
        
        Destroy(gameObject);
    }
}
