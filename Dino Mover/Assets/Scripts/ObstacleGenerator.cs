using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstacle;
    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject _obstacleSpawnPos;
    [SerializeField]
    float count = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleGen();
    }

    // Update is called once per frame
    void Update()
    {
        count = count - Time.deltaTime;
        if (count <= 0.0f)
        {
            count = 4.0f;
            ObstacleGen();
        }

    }
    void ObstacleGen()
    {
        int i = Random.Range(0, obstacle.Length);
        Instantiate(obstacle[i], new Vector3(Random.Range(cam.transform.position.x + 10, cam.transform.position.x + 15), _obstacleSpawnPos.transform.position.y, 0.0f), Quaternion.identity);

    }
}
