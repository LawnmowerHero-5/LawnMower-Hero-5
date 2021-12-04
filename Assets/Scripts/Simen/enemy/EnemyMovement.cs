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

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.targetTransform.position) <= range)
        {
            // Move our position a step closer to the target
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.targetTransform.position, step);
            print("Player spotted, chase started");
        }
        else
        {
            print("Where are you player?");
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