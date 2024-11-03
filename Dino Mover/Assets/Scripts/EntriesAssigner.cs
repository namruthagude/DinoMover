using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntriesAssigner : MonoBehaviour
{
    [SerializeField]
    TMP_Text _rank;
    [SerializeField]
    TMP_Text _name;
    [SerializeField]
    TMP_Text _score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SettingEntry(int rank, string name, int score)
    {
        _rank.text = rank.ToString();
        _name.text = name;
        _score.text = score.ToString();
    }
}
