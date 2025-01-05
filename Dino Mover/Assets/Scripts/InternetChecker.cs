using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject internetConnector;
    //Method to check device internet 
    public static bool IsConnectedToInternet()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Not connected to internet");
            return false;
        }

        Debug.Log("Connected to internet");
        return true;
    }

    public void RetryPanel()
    {
        internetConnector.SetActive(false);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internetConnector.SetActive(true);
        }
    }
}
