using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityJumppad : MonoBehaviour
{
    Animator playerAnim;

    public Animator anim;
    public GameObject player;
    public float jumppadForce = 7f;

    bool isInTrigger;

    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        if (playerAnim.GetBool("jumping"))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        else if (playerAnim.GetBool("falling") && !isInTrigger)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((collision.transform.position.y - transform.position.y) >= 0.68f)
            {
                anim.SetBool("isActived", true);
                collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, jumppadForce);
                GameObject.Find("AudioJumppad").GetComponent<AudioSource>().Play();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isInTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
    }
}
