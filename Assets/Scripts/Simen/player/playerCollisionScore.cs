using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore : MonoBehaviour
{
    public transformVariable score;
    private scoreController _scoreController;
    
    private void Start()
    {
        _scoreController = GetComponent<scoreManager>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wasp"))
        {
            score.score += _scoreController.killPointsWasp;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("EvilGnome"))
        {
            score.score += _scoreController.killPointsGnome;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bee"))
        {
            score.score -= _scoreController.loosePointsBee;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("GoodGnome"))
        {
            score.score -= _scoreController.loosePointsGoodGnome;
            Destroy(other.gameObject);
        }
    }
}
