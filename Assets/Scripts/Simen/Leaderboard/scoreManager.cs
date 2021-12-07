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
    
    [Header("Points")] [Space(5)] 
    [Range(0, 1000)] public int killPointsWasp;
    [Range(0, 1000)] public int killPointsGnome;
    [Range(0, 1000)] public int loosePointsBee;
    [Range(0, 1000)] public int loosePointsGoodGnome;


    private void Update()
    {
        SetCountText();
    }
    

    void SetCountText()
    {
        countText.text = "Score: " + score.score.ToString();
    }
}
