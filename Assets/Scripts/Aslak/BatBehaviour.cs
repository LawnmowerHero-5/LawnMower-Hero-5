using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;

public class BatBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float batDamage =  3f;

    private Transform trns;
    
    void Start()
    {
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
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            rb.useGravity = true;
        }
        else if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            rb.useGravity = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("EvilGnome") || 
            (other.collider.CompareTag("Gnome Lair")) ||
            (other.collider.CompareTag("GoodGnome")) && rb.velocity.sqrMagnitude is >= 10 and <= 14)
        {
            GetEnemyDoDamage(other, 1f);
            print("Im going Fast");
        }
        else if (other.collider.CompareTag("EvilGnome") || 
                 (other.collider.CompareTag("Gnome Lair")) ||
                 (other.collider.CompareTag("GoodGnome")) && rb.velocity.sqrMagnitude is >= 15 and <= 20)
        {
            GetEnemyDoDamage(other, 2f);

            print("Do you have anny idea how fast im going");
        }
        else if (other.collider.CompareTag("EvilGnome") || 
                 (other.collider.CompareTag("Gnome Lair")) ||
                 (other.collider.CompareTag("GoodGnome")) && rb.velocity.sqrMagnitude >= 21)
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