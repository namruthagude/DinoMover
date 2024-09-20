using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       

        //Setting Lives
        if (PlayerPrefs.HasKey("Lives"))
        {
            lives = PlayerPrefs.GetInt("Lives");
        }
        else
        {
            PlayerPrefs.SetInt("Lives", 5);
            lives = PlayerPrefs.GetInt("Lives");
        }

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
            if(PlayerPrefs.HasKey("RemainingTime")){
                float seconds = (float)(timePassed.TotalMinutes) * 60 ;

                // Caluculating previous remaining time and last time logged of
                if(seconds > PlayerPrefs.GetFloat("RemainingTime")){
                    seconds = seconds - PlayerPrefs.GetFloat("RemainingTime");
                    lives++;
                    float minutes = seconds / 60;
                    int livesToadd = Mathf.FloorToInt(minutes / lifeIncreaseInterval);
                    if (lives < maxLives)
                    {
                        lives += livesToadd;
                        if (lives > maxLives)
                        {
                            lives = maxLives;
                        }
                        UpdateLives();

                    }
                }
                else{
                    // removing remainingTime when logged out and increase life
                    remainingTime = PlayerPrefs.GetFloat("RemainingTime") - seconds;
                    
                }

                
            }
            else{
                SaveLastLifeUpdateTime();
            }
           
            

            //SaveLastLifeUpdateTime();
        }
        else{
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
    }

    void OnApplicationQuit()
    {
        // saving current time in player prefs 
        SaveLastLifeUpdateTime();

        //saving remaining time which will complete 1 life time
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
        
    }
}
