using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject ground;
    [SerializeField]
    float countdown = 1.0f;
    [SerializeField]
    GameObject _groundSpawnPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown = countdown - Time.deltaTime;
        if (countdown <= 0.0f)
        {
            countdown = 1.0f;
            GroundGen();
        }
    }
    void GroundGen()
    {
        Instantiate(ground, new Vector3(ground.transform.position.x + 40.0f, _groundSpawnPos.transform.position.y, ground.transform.position.z), Quaternion.identity);
    }
}
