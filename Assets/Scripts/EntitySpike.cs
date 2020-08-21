using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpike : MonoBehaviour
{
    public GameObject player;

    Animator playerAnim;

    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 玩家受到伤害
            playerAnim.SetBool("hurt", true);
        }
    }
}
