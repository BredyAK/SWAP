using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Animator playerAnim;

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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetComponent<Collider2D>().isTrigger = true;
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
