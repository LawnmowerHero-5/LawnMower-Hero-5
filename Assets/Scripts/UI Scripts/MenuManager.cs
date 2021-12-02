using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button nextButton;
    public Button previousButton;

    //Used to determine selected difficulty
    private int levelIndex = 0;

    //Difficulty UI text
    public string[] levelText;
    public TMP_Text levelTMPText;

    //The different menus
    public GameObject[] gamemodes;
    public GameObject[] leaderboardlevels;
    public GameObject[] otherOptionsMenu;
    
    
    void Update()
    {
        levelTMPText.text = levelText[levelIndex];
        print(levelIndex);
        
        selectedLevel();
        
        SelectedScoreboard();
    }

    public void NextLevel()
    {
        if (levelIndex < 2)
        {
            levelIndex++;
        }
        else
        {
            levelIndex = 0;
        }
    }

    public void PreviousLevel()
    {
        if (levelIndex > 0)
        {
            levelIndex--;
        }
        else
        {
            levelIndex = 2;
        }
    }

    private void selectedLevel()
    {
        if (levelIndex == 0)
        {
            leaderboardlevels[0].SetActive(true);
            leaderboardlevels[1].SetActive(false);
            leaderboardlevels[2].SetActive(false);
        }
        else if (levelIndex == 1)
        {
            leaderboardlevels[0].SetActive(false);
            leaderboardlevels[1].SetActive(true);
            leaderboardlevels[2].SetActive(false);
        }
        if (levelIndex == 2)
        {
            leaderboardlevels[0].SetActive(false);
            leaderboardlevels[1].SetActive(false);
            leaderboardlevels[2].SetActive(true);
        }
    }

    private void SelectedScoreboard()
    {
        if (levelIndex == 0)
        {
            gamemodes[0].SetActive(true);
            gamemodes[1].SetActive(false);
            gamemodes[2].SetActive(false);
        }
        else if (levelIndex == 1)
        {
            gamemodes[0].SetActive(false);
            gamemodes[1].SetActive(true);
            gamemodes[2].SetActive(false);
        }
        if (levelIndex == 2)
        {
            gamemodes[0].SetActive(false);
            gamemodes[1].SetActive(false);
            gamemodes[2].SetActive(true);
        }
    }

    public void BackToOtherOptions()
    {
        otherOptionsMenu[0].SetActive(true);
        otherOptionsMenu[1].SetActive(false);
        otherOptionsMenu[2].SetActive(false);
    }

    public void VolumeMenu()
    {
        otherOptionsMenu[0].SetActive(false);
        otherOptionsMenu[1].SetActive(true);
        otherOptionsMenu[2].SetActive(false);
    }

    public void CreditsMenu()
    {
        otherOptionsMenu[0].SetActive(false);
        otherOptionsMenu[1].SetActive(false);
        otherOptionsMenu[2].SetActive(true);
    }

    public void BackToLevelSelect()
    {
        SelectedScoreboard();
        gamemodes[3].SetActive(true);
        gamemodes[4].SetActive(false);
    }

    public void SelectLevel(int selectedlevel)
    {
        gamemodes[0].SetActive(false);
        gamemodes[1].SetActive(false);
        gamemodes[2].SetActive(false);
        gamemodes[3].SetActive(false);
        gamemodes[4].SetActive(true);
    }
}
