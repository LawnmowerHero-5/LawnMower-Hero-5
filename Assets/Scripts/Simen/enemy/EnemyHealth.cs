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
    public VisualEffect _Effect;
    private scoreManager _scoreManager;

    private void Start()
    {
        _Effect.Stop();
        _scoreManager = GetComponent<scoreManager>();
    }

    private void Update()
    {
        if (health is 0 or < 0)
        {
            _Effect.Play();
            Destroy(_Effect, 3f);
            _Effect.transform.parent = null;
          
            if (gameObject.CompareTag("EvilGnome"))
            {
                _scoreManager.score.score += _scoreManager.killPointsGnome;
            }
            else if (gameObject.CompareTag("Wasp"))
            {
                _scoreManager.score.score += _scoreManager.killPointsWasp;
            }
            else if (gameObject.CompareTag("GoodGnome"))
            {
                _scoreManager.score.score -= _scoreManager.loosePointsGoodGnome;
            }
            else if (gameObject.CompareTag("Bee"))
            {
                _scoreManager.score.score -= _scoreManager.loosePointsBee;
            }
            
            Destroy(gameObject);
            //StartCoroutine(destroyEnemy());
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
            /*_Effect.Play();
            Destroy(_Effect, 3f);
            _Effect.transform.parent = null;
            Destroy(gameObject);*/
            
            print("is realy supposed to be dead rn");
        }
    }
}