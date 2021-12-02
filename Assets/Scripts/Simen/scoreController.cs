using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class scoreController : MonoBehaviour
{
    public TMP_Text countText;
    public transformVariable score;


    private void Update()
    {
        SetCountText();
    }
    

    void SetCountText()
    {
        countText.text = "Score: " + score.score.ToString();
    }
}
