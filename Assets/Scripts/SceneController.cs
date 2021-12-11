using PlayerPreferences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private DataController _Data;
    
    public void LoadScene(string sceneName)
    {
        _Data.SetPlayerData();
        SceneManager.LoadScene(sceneName);
    }
    
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    }