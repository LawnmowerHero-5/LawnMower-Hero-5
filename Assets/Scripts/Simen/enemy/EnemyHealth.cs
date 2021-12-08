using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    private Rigidbody _rigibody;
    public VisualEffect _Effect;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        
        
    }

    private void Update()
    {
        if (health is 0 or < 0)
            
        {
            _Effect.Play();
            Destroy(_Effect, 3f);
            _Effect.transform.parent = null;
            Destroy(gameObject);
            //StartCoroutine(destroyEnemy());
        }
        else
        {
            _Effect.Stop();
        }
        
        
    }

   /* IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds((float) 0.25);
        Destroy(gameObject);
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            print("I Should Be Dead Right Now");
        }
    }
}