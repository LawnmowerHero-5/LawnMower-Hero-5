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
    [SerializeField] private int _frameInterval = 100;
    private Vector3 followTarget;

    private void Start()
    {
        Position();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, followTarget) <= range)
        {
            // Move our position a step closer to the target
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, followTarget, step);
            print("Player spotted, chase started");
        }
        else
        {
            print("Where are you player?");
        }
        
        
        if (Time.frameCount % _frameInterval == 0)
        {
            Position();
        }
    }
    
    private void Position()
    {
        followTarget = target.playerTransform.position;
        _frameInterval = 100;
        print("followTarget");
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}