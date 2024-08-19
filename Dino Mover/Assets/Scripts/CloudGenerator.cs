using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] cloud;
    [SerializeField]
    Camera cam;
    [SerializeField]
    float count = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        CloudGen();
    }

    // Update is called once per frame
    void Update()
    {
        count = count - Time.deltaTime;
        if (count <= 0.0f){
            count = 7.0f;
            CloudGen();
        }
       
    }
    void  CloudGen(){
        int i = Random.Range(0, cloud.Length);
        Instantiate(cloud[i], new Vector3(Random.Range(cam.transform.position.x +10, cam.transform.position.x + 15), Random.Range(1.5f,2.0f), 0.0f), Quaternion.identity);
       
    }
}
