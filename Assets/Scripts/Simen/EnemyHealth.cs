using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    private Rigidbody _rigibody;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}