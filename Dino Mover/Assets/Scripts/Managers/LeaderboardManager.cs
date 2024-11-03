using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;
using System;
public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    [SerializeField]
    GameObject[] entries;
    [SerializeField]
    GameObject rank;
    
    [SerializeField]
    TMP_InputField username;
    [SerializeField]
    GameObject settingUsernamePanel;
    [SerializeField]
    GameObject leaderBoardPanel;

    private string coinsKey = "5a3c6604e10c124e4786ceb5268ab7fd89b3273b97f20770388427b52de652aa";
    private string distanceKey = "344eb9f4eedf9abd53883b90605c6e7b336b7037361c9d44e3f392b95a8a6fbf";

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //Checking player set name for leaderboard or not 
        CheckingPlayerExists();

        // Loading COin Leaderboard
        LoadCoinLeaderBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
     
    void GetCoinLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(coinsKey, (msg) =>
        {
            int length = Math.Min(entries.Length, msg.Length);
            for (int i = 0; i < length; i++)
            {
                entries[i].SetActive(true);
                EntriesAssigner entry = entries[i].GetComponent<EntriesAssigner>();
                entry.SettingEntry(msg[i].Rank, msg[i].Username, msg[i].Score);
            }
        });
    }

    void GetDistanceLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(distanceKey, (msg) =>
        {
            int length = Math.Min(entries.Length, msg.Length);
            for (int i = 0; i < length; i++)
            {
                entries[i].SetActive(true);
                EntriesAssigner entry = entries[i].GetComponent<EntriesAssigner>();
                entry.SettingEntry(msg[i].Rank, msg[i].Username, msg[i].Score);
            }
        });
    }
    void UpdateEntries()
    {
       
    }

    public void SetDistanceEntry(string name, int score)
    {   
        LeaderboardCreator.UploadNewEntry(distanceKey,name, score, (_) =>
        {
           GetDistanceLeaderboard();
        });
    }
    public void SetCoinsEntry(string name, int coins)
    {
        LeaderboardCreator.UploadNewEntry(coinsKey, name, coins, (_) =>
        {
            GetCoinLeaderboard();
        });
    }
    void LoadCoinRank()
    {
        LeaderboardCreator.GetLeaderboard(coinsKey, (msg) =>
        {

            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i].Username == PlayerPrefs.GetString("PlayerName"))
                {
                    EntriesAssigner entry = rank.GetComponent<EntriesAssigner>();
                    entry.SettingEntry(msg[i].Rank, msg[i].Username, msg[i].Score);
                    break;
                }

            }
        });
    }

    void LoadDistanceRank()
    {
        LeaderboardCreator.GetLeaderboard(distanceKey, (msg) =>
        {

            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i].Username == PlayerPrefs.GetString("PlayerName"))
                {
                    EntriesAssigner entry = rank.GetComponent<EntriesAssigner>();
                    entry.SettingEntry(msg[i].Rank, msg[i].Username, msg[i].Score);
                    break;
                }

            }
        });
    }

    void SettingPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
    }

    public void CheckingPlayerExists()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
           settingUsernamePanel.SetActive(true);
        }
    }

    public void SetUsername()
    {
        ClosePanel();
        SettingPlayerName(username.text);
    }

    public void ClosePanel()
    {
        settingUsernamePanel.SetActive(false);
    }

    void DisablingEntries()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].SetActive(false);
        }
    }

    public void LoadCoinLeaderBoard()
    {
        DisablingEntries();
        GetCoinLeaderboard();
        LoadCoinRank();
    }

    public void LoadDistanceLeaderBoard()
    {
        DisablingEntries();
        GetDistanceLeaderboard();
        LoadDistanceRank();
    }

    public void OpenLeaderBoard()
    {
        leaderBoardPanel.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        leaderBoardPanel.SetActive(false);
    }
}
