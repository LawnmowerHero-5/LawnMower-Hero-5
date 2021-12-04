using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore : MonoBehaviour
{
    public transformVariable score;
    [Header("Points")]
    [Space(5)]
    [Range(0, 1000)] public int killPointsWasp;
    [Range(0, 1000)] public int killPointsGnome;
    [Range(0, 1000)] public int loosePointsBee;
    [Range(0, 1000)] public int loosePointsGoodGnome;
    

    private void Start()
    {
        score.score = 0;
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wasp"))
        {
            score.score += killPointsWasp;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("EvilGnome"))
        {
            score.score += killPointsGnome;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bee"))
        {
            score.score -= loosePointsBee;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("GoodGnome"))
        {
            score.score -= loosePointsGoodGnome;
            Destroy(other.gameObject);
        }
    }
}
