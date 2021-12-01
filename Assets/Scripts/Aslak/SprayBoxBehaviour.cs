using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBoxBehaviour : MonoBehaviour
{
    public EnemyHealth EnemyHealth;
    public float SprayDamage = 3f;
    void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
   

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            WaitForSeconds(3);
            
            print("NOT THE BEES");
        }
    }
}
