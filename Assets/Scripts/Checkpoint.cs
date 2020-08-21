using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gm;
    Animator anim;
    bool isActived;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        if (transform.position.x < gm.lastCheckpointPos.x + 1)
        {
            isActived = true;
            anim.SetBool("isActived", true);
        }
    }

    public void CheckpointActived()
    {
        anim.SetBool("isActived", true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActived)
        {
            GameObject.Find("AudioCheckpoint").GetComponent<AudioSource>().Play();
            anim.SetBool("isReached", true);
            GameObject.Find("Canvas").GetComponent<GameText>().SaveScore();
            isActived = true;
            gm.lastCheckpointPos = transform.position;
        }
    }
}
