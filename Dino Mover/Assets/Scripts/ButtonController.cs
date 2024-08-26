using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    TMP_Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        CalculateCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRetryClick()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Coins", GameManager.Coins);
    }

    public void IncreaseLives()
    {
        AdsManager.instance.ShowLivesAd();
    }

    public void DoubleCoins()
    {
        AdsManager.instance.ShowCoinsAd();
    }
    void CalculateCoins()
    {
        if(GameManager.Coins != null)
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                int coins = GameManager.Coins - PlayerPrefs.GetInt("Coins");
                coinsText.text = "You collected " + coins.ToString() + " Coins"; 
            }
            else
            {
                int coins = GameManager.Coins;
                coinsText.text = "You collected " + coins.ToString() + " Coins";
            }
            
        }
    }
}
