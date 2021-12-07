using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Movement towards player
    public float speed;
    public transformVariable target;
    public float range = 10f;

    public bool inCombat;

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.playerTransform.position) <= range)
        {
            // Move our position a step closer to the target
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.playerTransform.position, step);
            print("Player spotted, chase started");
            inCombat = true;
        }
        else
        {
            print("Where are you player?");
            inCombat = false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}