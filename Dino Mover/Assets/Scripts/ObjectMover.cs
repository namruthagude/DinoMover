using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    GameObject objectToMove;
    [SerializeField]
    float speed;
    [SerializeField]
    float speedIncrease = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Speed += speedIncrease * Time.deltaTime;
        transform.position = new Vector3(transform.position.x - GameManager.Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
