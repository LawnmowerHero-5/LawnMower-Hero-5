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

    #region PointSystem
    
    [Header("Point system")]
    [Header("Gain Points")] [Space(5)]
    [Range(0, 100)] public int killPointsWasp;
    [Range(0, 100)] public int killPointsGnome;
    
    [Header("Loose Points")] [Space(5)] 
    [Range(0, 100)] public int loosePointsBee;
    [Range(0, 100)] public int loosePointsGoodGnome;
    
    [Header("Point per % off grass cut")] [Space(5)] 
    [Range(2, 10)] public int grassPoints;
    
    #endregion
    
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