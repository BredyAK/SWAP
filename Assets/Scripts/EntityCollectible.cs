using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollectible : MonoBehaviour
{
    Animator anim;

    bool isCollected; // 实体是否已被收集

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void FixedUpdate()
    {
        if (isCollected) // 是否已收集
        {
            GameObject.Find("AudioCollected").GetComponent<AudioSource>().Play();
            anim.SetBool("isCollected", true);
            gameObject.SetActive(false);
            GameObject.Find("Canvas").GetComponent<GameText>().EntityColledted(); // 增加分数
            isCollected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollected = true;
        }
    }
}
