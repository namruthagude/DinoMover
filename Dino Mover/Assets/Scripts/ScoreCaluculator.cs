using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCaluculator : MonoBehaviour
{
    [SerializeField]
    PauseManager pauseManager;
    [SerializeField]
    TMP_Text score;
    float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseManager.ISPaused())
        {
            count = count + 0.02f;

            int temp = (int)count;

            score.SetText(temp.ToString());
        }
        
    }
}
