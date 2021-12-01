using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BatBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float batDamage =  3f;
    public EnemyHealth EnemyHealth;
    
    
    void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
    }

    [ExecuteInEditMode]
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Velocity: "+ rb.velocity.sqrMagnitude);
    }

    // Update is called once per frame
    void Update()
    {
        
       /* if (gameObject.CompareTag("Enemy") && rb.velocity.sqrMagnitude is >= 10 and <= 14)
        {
            EnemyHealth.health --;
            print("Im going Fast");
        }
        else if (gameObject.CompareTag("Enemy") && rb.velocity.sqrMagnitude is >= 15 and <= 20)
        {
            EnemyHealth.health --;
            print("Do you have anny idea how fast im going");
        }
        else if (gameObject.CompareTag("Enemy") && rb.velocity.sqrMagnitude >= 21)
        {
            EnemyHealth.health --;
            print("Fast AF boyyy");
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy") && rb.velocity.sqrMagnitude is >= 10 and <= 14)
        {
           EnemyHealth.health -= batDamage;
            print("Im going Fast");
        }
        else if (other.collider.CompareTag("Enemy") && rb.velocity.sqrMagnitude is >= 15 and <= 20)
        {
            EnemyHealth.health -= batDamage*2;
            print("Do you have anny idea how fast im going");
        }
        else if (other.collider.CompareTag("Enemy") && rb.velocity.sqrMagnitude >= 21)
        {
            EnemyHealth.health -= batDamage*3;
            print("Fast AF boyyy");
        }
    }
}

    

