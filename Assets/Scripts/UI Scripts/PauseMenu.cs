using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject[] pauseMenuMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void BackToToPauseMenu()
    {
        pauseMenuMenu[0].SetActive(true);
        pauseMenuMenu[1].SetActive(false);
        pauseMenuMenu[2].SetActive(false);
    }

    public void PauseVolumeMenu()
    {
        pauseMenuMenu[0].SetActive(false);
        pauseMenuMenu[1].SetActive(true);
        pauseMenuMenu[2].SetActive(false);
    }
    public void ConfirmQuit()
    {
        pauseMenuMenu[0].SetActive(false);
        pauseMenuMenu[1].SetActive(false);
        pauseMenuMenu[2].SetActive(true);
    }
}
