using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    [SerializeField]
    TMP_Text _lives;
    [SerializeField]
    TMP_Text timerText;
    float remainingTime = 300;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update entered");
        if (PlayerPrefs.HasKey("Lives"))
        {
            if (_lives.text != PlayerPrefs.GetInt("Lives").ToString())
            {
                _lives.text = PlayerPrefs.GetInt("Lives").ToString();
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowAchievements()
    {
        Debug.Log("Showing Achievements");
    }
    void UpdateLives()
    {
        _lives.text = PlayerPrefs.GetInt("Lives").ToString();
    }
    public void SettingCountDown(float time)
    {
        remainingTime = time;
        //Enabling Timer
        timerText.gameObject.SetActive(true);

        //Setting Countdown Time
        if (remainingTime > 0)
        {
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00} :{1:00}", min, sec);
        }
        else
        {
            remainingTime = 300;
           
        }
    }
    public void DisableTimer()
    {
        timerText.gameObject.SetActive(false);
    }
}
