using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

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
            print("Look how they masacerd my boy");
            
            VisualEffect.Send
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            print("I Should Be Dead Right Now");
        }
    }
}