using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning;
    public bool canSubmitScore;
    public TMP_Text timer;
    private playFabManagerIntermediate2 _intermediate2;
    private pauseEffect _pauseMenu;

    private void Start()
    {
        timerIsRunning = true;
        _intermediate2 = GetComponent<playFabManagerIntermediate2>();
        _pauseMenu = GetComponent<pauseEffect>();

    }

    private void Update()
    {
        DisplayTime(timeRemaining);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                canSubmitScore = true;
                _pauseMenu.Pause();
                timerIsRunning = false;
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}