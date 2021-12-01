using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    private Rigidbody _rigibody;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (health is 0 or < 0)
        {
            Destroy(gameObject);
        }
    }
}