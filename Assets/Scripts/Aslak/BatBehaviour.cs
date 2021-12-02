using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Valve.VR;

public class BatBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float batDamage =  3f;
    public EnemyHealth EnemyHealth;

    private Transform trns;
    
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
        if (other.collider.CompareTag("Gnome") && rb.velocity.sqrMagnitude is >= 10 and <= 14)
        {
           /* if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                EnemyHealth.health -= batDamage;
            }
            */ 
           
           GetEnemyDoDamage(other, 1f);
            print("Im going Fast");
        }
        else if (other.collider.CompareTag("Gnome") && rb.velocity.sqrMagnitude is >= 15 and <= 20)
        {
            GetEnemyDoDamage(other, 2f);

            print("Do you have anny idea how fast im going");
        }
        else if (other.collider.CompareTag("Gnome") && rb.velocity.sqrMagnitude >= 21)
        {
            GetEnemyDoDamage(other,  3f);
            print("Fast AF boyyy");
        }
    }

    private void GetEnemyDoDamage(Collision gnome, float damage)
    {
        if (gnome.collider.GetComponent<EnemyHealth>() != null)
        {
            gnome.collider.GetComponent<EnemyHealth>().health -= batDamage * damage;
        }
    }
}

    

