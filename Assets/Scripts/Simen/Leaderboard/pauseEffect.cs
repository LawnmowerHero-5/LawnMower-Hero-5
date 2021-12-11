using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class pauseEffect : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject PauseMenuUI;

    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
