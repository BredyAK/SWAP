using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    GameManager gm;
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    AudioSource audioJump;
    float horizontalMove;

    public float speed, jumpForce;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump, isDashing;

    bool jumpPressed;
    int jumpCount;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioJump = GameObject.Find("AudioPlayerJump").GetComponent<AudioSource>();
        transform.position = gm.lastCheckpointPos;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && jumpCount > 0)
        {
            jumpPressed = true;
        }

        // 按键重生
        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.Respawn();
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        GroundMovement();

        Jump();
        
        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal"); // 只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }

    void Jump() // 跳跃
    {
        if (isGround)
        {
            jumpCount = 2; // 可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            audioJump.Play();
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            audioJump.Play();
        }
    }

    void SwitchAnim() // 动画切换
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

    public void PlayerDead()
    {
        gm.Respawn();
    }

    public void PlaySoundHurt()
    {
        GameObject.Find("AudioPlayerHurt").GetComponent<AudioSource>().Play();
    }
}
