using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using System.IO;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    public  int lives ;

    public int maxLives = 5;
    public int lifeIncreaseInterval = 5;
    private string _lastlifeUpdateTimeKey = "lastlifeupdatetime";
    private float _timer = 0;
    private float remainingTime = 300;
    
    private void Awake()
    {
        

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //Checking fresh install or not
        CheckInstall();
        //Setting Lives
        CheckingLives();

        //Calculating Lives
        CaluculateLives();
    }
    private void Start()
    {
       Debug.Log("Lives" +lives);
       
    }
    private void Update()
    {
        if(lives < maxLives)
        {
           _timer += Time.deltaTime;
            SettingTimer();
            if(_timer >= lifeIncreaseInterval * 60)
            {
                IncreaseLife();
                _timer = 0;
            }
        }
       
    }

    void CheckingLives()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            Debug.LogError("Has key");
            lives = PlayerPrefs.GetInt("Lives");
            Debug.LogError(lives);
        }
        else
        {
            Debug.LogError(" Does not had key");
            PlayerPrefs.SetInt("Lives", 5);
            PlayerPrefs.Save();
            lives = PlayerPrefs.GetInt("Lives");
            Debug.LogError(lives);
        }
    }

    public void SettingTimer()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if(MainMenuManager.Instance != null)
            {
                MainMenuManager.Instance.SettingCountDown(remainingTime);
            }
            if(GameManager.Instance != null)
            {
                GameManager.Instance.SettingCountDown(remainingTime);
            }
        }
        else
        {
            remainingTime = 300;
            if (MainMenuManager.Instance != null)
            {
                MainMenuManager.Instance.DisableTimer();
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DisableTimer();
            }
        }
    }
    public int ShowLives()
    {
        return lives;
    }
    void CheckInstall() {
        string path = Path.Combine(Application.persistentDataPath, "install_flag.txt");

        if (File.Exists(path))
        {
            // File exists, so this is an existing installation
            Debug.Log("App opened - existing installation");
        }
        else
        {
            
            // File doesn't exist, so this is a fresh install
            Debug.Log("App opened - fresh install");
            PlayerPrefs.DeleteAll();  // Use only for testing
            PlayerPrefs.Save();
            // Create the file to mark the app as installed
            File.WriteAllText(path, "installed");
        }
    }
    public void IncreaseLife()
    {
        if(lives < maxLives)
        {
            lives++;
            UpdateLives();
            SaveLastLifeUpdateTime();
        }
        

    }
    public void DecreaseLife()
    {
        if(lives > 0) {
            lives--;
            UpdateLives();
           
        }
        
    }

    void CaluculateLives()
    {
        if (PlayerPrefs.HasKey(_lastlifeUpdateTimeKey))
        {
            string lastUpdatedTimeString = PlayerPrefs.GetString(_lastlifeUpdateTimeKey);
            DateTime lastUpdateTime = DateTime.Parse(lastUpdatedTimeString);
            TimeSpan timePassed = DateTime.Now - lastUpdateTime;

            if (PlayerPrefs.HasKey("RemainingTime"))
            {
                float secondsPassed = (float)timePassed.TotalSeconds;
                float storedRemainingTime = PlayerPrefs.GetFloat("RemainingTime");

                if (secondsPassed >= storedRemainingTime)
                {
                    secondsPassed -= storedRemainingTime;
                    lives++;
                    int livesToAdd = Mathf.FloorToInt(secondsPassed / (lifeIncreaseInterval * 60));
                    lives = Mathf.Min(maxLives, lives + livesToAdd);
                    UpdateLives();
                }
                else
                {
                    remainingTime = storedRemainingTime - secondsPassed;
                }
            }
            else
            {
                remainingTime = 300; // Set to default if it's the first time
                SaveLastLifeUpdateTime();
            }
        }
        else
        {
            remainingTime = 300;
            SaveLastLifeUpdateTime();
        }
    }


    void SaveLastLifeUpdateTime()
    {
        PlayerPrefs.SetString(_lastlifeUpdateTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
    void UpdateLives()
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        // saving current time in player prefs 
        SaveLastLifeUpdateTime();

        //saving remaining time which will complete 1 life time
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
        PlayerPrefs.Save();

    }
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused) // App is going into the background
        {
            // Save any important data here
            PlayerPrefs.SetInt("Lives", lives);  // Example of saving lives
            PlayerPrefs.SetFloat("RemainingTime", remainingTime);  // Example of saving remaining time
            SaveLastLifeUpdateTime();  // Any other custom save function you might have
            PlayerPrefs.Save();  // Ensure PlayerPrefs data is written to disk
        }
        else
        {
            Debug.Log("App resumed");
            CheckingLives();
            CaluculateLives();
        }
    }

}
