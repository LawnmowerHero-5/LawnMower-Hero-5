using System;
using PlayerPreferences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private DataController _Data;
    private float timer;
    private string action;
    private string targetScene;

    public static event Action swappedScene;
    
    public void LoadScene(string sceneName)
    {
        swappedScene?.Invoke();
        _Data.SetPlayerData();
        targetScene = sceneName;
        timer = 0.05f;
        action = "load";
    }
    
    public void ResetScene()
    {
        swappedScene?.Invoke();
        _Data.SetPlayerData();
        timer = 0.05f;
        action = "reset";
    }

    public void QuitGame()
    {
        swappedScene?.Invoke();
        _Data.SetPlayerData();
        timer = 0.05f;
        action = "quit";
    }

    private void FixedUpdate()
    {
        if (timer > 0) timer -= Time.fixedDeltaTime;
        else
        {
            if (action == "load") SceneManager.LoadScene(targetScene);
            else if (action == "quit") Application.Quit();
            else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}