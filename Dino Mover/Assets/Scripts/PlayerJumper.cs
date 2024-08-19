using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJumper : MonoBehaviour
{
    // Start is called before the first frame update
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


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            run.enabled = false;
            rb.velocity = Vector3.up * jumpForce;
            StartCoroutine(JumpRoute());
            
        }
    }
    IEnumerator JumpRoute()
    {
        animator.Play("jump");
        jump.Play();
        yield return new WaitForSeconds(1);
        animator.Play("run");
        run.enabled = true;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "obstacle")
        {
            StartCoroutine(Dead());
        }
    }
    IEnumerator Dead()
    {
        gameover.Play();
        animator.Play("dead");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        SceneManager.LoadScene(1);
    }
}
