using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int Coins = 0;
    public static float Speed = 5; 
    [SerializeField]
    TMP_Text _lives;
    [SerializeField]
    TMP_Text timerText;
    [SerializeField]
    TMP_Text coinsValue;
    float remainingTime = 300;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins = PlayerPrefs.GetInt("Coins");
            coinsValue.text = Coins.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 0);
           
        }
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
           if(_lives.text != PlayerPrefs.GetInt("Lives").ToString())
            {
                _lives.text = PlayerPrefs.GetInt("Lives").ToString();
            }
        }
    }

    void UpdateLives()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            _lives.text = PlayerPrefs.GetInt("Lives").ToString();
        }
    }
    public void SettingCountDown(float time)
    {
        //Enabling Timer
        timerText.gameObject.SetActive(true);
        remainingTime = time;
        //Setting Countdown Time
        if (remainingTime > 0)
        {
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00} :{1:00}", min, sec);
        }
        
    }
    public void DisableTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coins", GameManager.Coins);
    }
}
