using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField]
    TMP_Text _lives;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    AudioSource jump;
    [SerializeField]
    AudioSource run;
    [SerializeField]
    AudioSource gameover;
    [SerializeField]
    VibrationManager vibrationManager;
    [SerializeField]
    TMP_Text coins;
    bool _isJumping = false;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!_isJumping)
            {
                run.enabled = false;
                rb.velocity = Vector3.up * jumpForce;
                StartCoroutine(JumpRoute());
            }
           
            
        }
    }
    IEnumerator JumpRoute()
    {
        _isJumping = true;
        vibrationManager.HighVibration();
        animator.Play("jump");
        jump.Play();
        yield return new WaitForSeconds(1.25f);
        animator.Play("run");
        run.enabled = true;
        _isJumping = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Tag " + col.gameObject.tag);
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            FeedbackManager.Instance.CoinShake();
            vibrationManager.MediumVibration();
            Debug.Log("Collided with coin");
            coins.text = (int.Parse(coins.text) + 1).ToString();
            GameManager.Coins = GameManager.Coins + 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "obstacle")
        {
            if (LifeManager.Instance.ShowLives() > 1)
            {
                FeedbackManager.Instance.ObstacleShake();
                vibrationManager.HighVibration();
                Destroy(collision.gameObject);
                LifeManager.Instance.DecreaseLife();
                UpdateLives();
            }
            else
            {
                LeaderboardManager.Instance.SetCoinsEntry(PlayerPrefs.GetString("PlayerName"),GameManager.Coins);
                LeaderboardManager.Instance.SetDistanceEntry(PlayerPrefs.GetString("PlayerName"), GameManager.Score);
                PlayerPrefs.SetInt("Coins", GameManager.Coins);
                StartCoroutine(Dead());
            }
        }
    }
    IEnumerator Dead()
    {
        gameover.Play();
        FeedbackManager.Instance.GameOverShake();
        animator.Play("dead");
        vibrationManager.FailureVibration();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        SceneManager.LoadScene(2);
    }

    void UpdateLives()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            _lives.text = PlayerPrefs.GetInt("Lives").ToString();
        }
    }
}
