using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scoreController : MonoBehaviour
{
    public TMP_Text countText;
    public float score;
    [Header("Points")]
    [Space(5)]
    [Range(0, 1000)] public float killPointsWasp;
    [Range(0, 1000)] public float killPointsGnome;
    [Range(0, 1000)] public float loosePointsBee;

    private void Start()
    {
        score = 0;
        SetCountText();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wasp"))
        {
            score += killPointsWasp;
            SetCountText();
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Gnome"))
        {
            score += killPointsGnome;
            SetCountText();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bee"))
        {
            score -= loosePointsBee;
            SetCountText();
            Destroy(other.gameObject);
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + score.ToString();
    }
}
