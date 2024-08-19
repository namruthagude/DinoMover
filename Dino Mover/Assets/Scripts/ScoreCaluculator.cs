using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCaluculator : MonoBehaviour
{
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
        count = count + 0.01f;
        
         int temp = (int)count;
        
        score.SetText(temp.ToString());
    }
}
