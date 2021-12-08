using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public TMP_Text countText;
    public transformVariable score;
    public int totalScore;

    [Header("Points")] [Space(5)] 
    [Range(0, 100)] public int killPointsWasp;
    [Range(0, 100)] public int killPointsGnome;
    [Range(0, 100)] public int loosePointsBee;
    [Range(0, 100)] public int loosePointsGoodGnome;

    private void Start()
    {
        score.score = 0;
        score.score2 = 0;
    }

    private void Update()
    {
        SetCountText();
        print(score.score + score.score2);
        totalScore = (int) (score.score + score.score2);
    }
    
    void SetCountText()
    {
        countText.text = "Score: " + totalScore;
    }
}