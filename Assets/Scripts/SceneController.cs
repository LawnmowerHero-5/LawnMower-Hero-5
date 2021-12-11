using PlayerPreferences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static DataController _Data;
    
    public static void LoadScene(string sceneName)
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