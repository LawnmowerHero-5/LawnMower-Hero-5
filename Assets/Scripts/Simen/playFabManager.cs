using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Networking;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TMPro;
using Valve.VR;

public class playFabManager : MonoBehaviour
{
    #region Components

    [Header("Windows")] 
    public GameObject nameWindow;
    public GameObject leaderboardWindow;

    [Header("Display name window")] 
    public GameObject nameError;
    public TMP_InputField nameInput;
    
    [Header("Leaderboard")]
    public GameObject rowPreFab;
    public Transform rowParent;

    private string _loggedInPlayFabId;
    
    #endregion
    private void Start()
    {
        nameInput.text = "Enter your name";
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = "Tutorial",
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        _loggedInPlayFabId = result.PlayFabId;
        Debug.Log("Successful login/account create!");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            nameWindow.SetActive(true);
        }
        else
        {
            leaderboardWindow.SetActive(true);
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = "Easy",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "Easy",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderoardGet, OnError);
    }

    void OnLeaderoardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPreFab, rowParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("Place: {0} | ID: {1} | VALUE: {2}", item.Position, item.PlayFabId, item.StatValue));
        }
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest()
        {
            StatisticName = "Easy",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayer, OnError);
    }
    
    void OnLeaderboardAroundPlayer(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPreFab, rowParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == _loggedInPlayFabId)
            {
                texts[0].color = Color.magenta;
                texts[1].color = Color.magenta;
                texts[2].color = Color.magenta;
            }

            Debug.Log(string.Format("Place: {0} | ID: {1} | VALUE: {2}", item.Position, item.PlayFabId, item.StatValue));
        }
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        leaderboardWindow.SetActive(true);
    }
}