using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEnemy : MonoBehaviour
{
    Animator anim;
    Animator playerAnim;
    AudioSource audioDeath;

    bool isDestroyed; // 实体是否已被消灭

    public float bounceForce = 10f;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        audioDeath = GameObject.Find("AudioEnemyDestroyed").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isDestroyed) // 是否已消灭
        {
            anim.SetBool("isDestroyed", true);
            gameObject.SetActive(false);
            GameObject.Find("Canvas").GetComponent<GameText>().EnemyDestroyed(); // 增加分数
            isDestroyed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y - transform.position.y >= 0.98f)
            {
                collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, bounceForce);
                audioDeath.Play();
                isDestroyed = true;
            }
            else
            {
                // 玩家受到伤害
                playerAnim.SetBool("hurt", true);
            }
        }
    }
}
