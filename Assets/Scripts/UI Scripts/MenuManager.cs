using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

public class MenuManager : MonoBehaviour
{
    [Header("Leaderboard Name Input Field")]
    public TMP_InputField textEntry;
    string text = "";

    [Space(5)]
    [Header("Difficulty select buttons")]
    public Button nextButton;
    public Button previousButton;

    //Used to determine selected difficulty
    private int levelIndex = 0;

    //Difficulty UI text
    [Space(5)]
    [Header("Difficulty select text and array")]
    public string[] levelText;
    public TMP_Text levelTMPText;

    [Space(7)]
    //The different menus
    public GameObject[] gamemodes;
    public GameObject[] leaderboardlevels;
    public GameObject[] otherOptionsMenu;

    void Update()
    {
        //Sets the easy/intermediate/hard text in the difficulty select UI
        levelTMPText.text = levelText[levelIndex];

        SelectedLevel();

        SelectedScoreboard();
        
        print(levelIndex);
        
    }



    private void OnEnable()
    {
        //Listens for keyboard clicks and if the keyboard closes
        SteamVR_Events.System(EVREventType.VREvent_KeyboardCharInput).Listen(OnKeyboard);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Listen(OnKeyboardClosed);
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

    private void SelectedLevel()
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

    //Called from UnityEvent button press
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

    // This code is being called from a UnityEvent (button/text field select)
    public void ShowKeyboard()
    {
        SteamVR.instance.overlay.ShowKeyboard(0, 0, 0, "Description", 256, "", 0);
        textEntry.text = "";
    }


    //Runs every SteamVR keyboard button press
    private void OnKeyboard(VREvent_t args)
    {
        print("Clicked something!");
        VREvent_Keyboard_t keyboard = args.data.keyboard;
        byte[] inputBytes = new byte[] { keyboard.cNewInput0, keyboard.cNewInput1, keyboard.cNewInput2, keyboard.cNewInput3, keyboard.cNewInput4, keyboard.cNewInput5, keyboard.cNewInput6, keyboard.cNewInput7 };
        int len = 0;
        for (; inputBytes[len] != 0 && len < 7; len++) ;
        string input = System.Text.Encoding.UTF8.GetString(inputBytes, 0, len);

            System.Text.StringBuilder textBuilder = new System.Text.StringBuilder(1024);
            uint size = SteamVR.instance.overlay.GetKeyboardText(textBuilder, 1024);
            text = textBuilder.ToString();
            print(text);
            textEntry.text = text;
    }

    private void OnKeyboardClosed(VREvent_t args)
    {
        // Might use this to unselect input field. Not sure yet
        //EventSystem.current.SetSelectedGameObject(null);
    }
}
