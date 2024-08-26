using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _coin;
    [SerializeField]
    Camera _camera;
    [SerializeField]
    GameObject _coinSpawnPos;
    [SerializeField]
    float countdown;
    // Start is called before the first frame update
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
            CoinGen();
        }
    }
    void CoinGen()
    {
        int numberOfCoins = Random.Range(1,2);
        BoxCollider2D collider = _coinSpawnPos.GetComponent<BoxCollider2D>();
        if(collider != null )
        {
            for(int i=0; i < numberOfCoins; i++)
            {
                Instantiate(_coin, new Vector3(Random.RandomRange(collider.bounds.min.x, collider.bounds.max.x), collider.bounds.center.y, 0f),Quaternion.identity);
            }
        }
    }
}
