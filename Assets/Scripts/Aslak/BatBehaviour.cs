using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BatBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float batDamage =  3f;
    
    
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
        
        if (rb.velocity.sqrMagnitude is >= 10 and <= 14)
        {
            
        }
        else if (rb.velocity.sqrMagnitude is >= 15 and <= 20)
        {
            gameObject.CompareTag("FastBat");
            print("Do you have anny idea how fast im going");
        }
        else if (rb.velocity.sqrMagnitude >= 21)
        {
            gameObject.CompareTag("FastestBat");
            print("Fast AF boyyy");
        }
    }

   
}

    

