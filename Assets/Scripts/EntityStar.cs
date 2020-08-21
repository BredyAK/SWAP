using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStar : MonoBehaviour
{
    Animator anim;

    bool isStarCollected; // 实体是否已被收集

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void FixedUpdate()
    {
        if (isStarCollected) // 是否已收集
        {
            GameObject.Find("AudioStarCollected").GetComponent<AudioSource>().Play();
            anim.SetBool("isStarCollected", true);
            gameObject.SetActive(false);
            GameObject.Find("Canvas").GetComponent<GameText>().EntityStarColledted(); // 增加分数
            isStarCollected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isStarCollected = true;
        }
    }
}
