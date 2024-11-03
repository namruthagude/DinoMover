using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance;
    [SerializeField]
    MMFeedbacks coinShake;
    [SerializeField]
    MMFeedbacks obstacleShake;
    [SerializeField]
    MMFeedbacks gameOverShake;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinShake()
    {
        Debug.Log("Coin Shake Feed back playing");
        coinShake.PlayFeedbacks();
    }

    public void ObstacleShake()
    {
        obstacleShake.PlayFeedbacks();
    }

    public void GameOverShake()
    {
        gameOverShake.PlayFeedbacks();
    }
}
