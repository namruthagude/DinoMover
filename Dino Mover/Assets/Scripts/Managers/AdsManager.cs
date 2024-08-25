using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    public IncreaseLives increaseLives;
    public DoubleCoins doubleCoins;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("Mobile Ads Intialized");
            LoadLivesAd();
            LoadCoinsAd();
        });
       
    }

    // Update is called once per frame
   void LoadLivesAd()
    {
        increaseLives.LoadRewardedAd();
    }

    public void ShowLivesAd()
    {
        increaseLives.ShowRewardedAd();
        LoadLivesAd();
    }

    void LoadCoinsAd()
    {
        doubleCoins.LoadRewardedAd();
    }

    public void ShowCoinsAd()
    {
        doubleCoins.ShowRewardedAd();
        LoadCoinsAd();
    }
}
