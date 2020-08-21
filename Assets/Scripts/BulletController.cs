using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    GameObject cam;
    public float speed = 20f;
    public float bulletDistance = 16f; // 子弹可超出屏幕中心的最大横向距离，避免击中屏幕外实体影响体验
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed * GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - cam.transform.position.x) > bulletDistance)
        {
            Destroy(gameObject); // 超出屏幕后移除子弹
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Star" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "CheckPoint" && collision.gameObject.tag != "Platform")
        {
            if (collision.gameObject.name == "Collectible" || collision.gameObject.name == "Spike" || collision.gameObject.name == "Block" || collision.gameObject.name == "Enemy" || collision.gameObject.name == "Jumppad")
            {
                GameObject.Find("Canvas").GetComponent<GameText>().MakeSelection(collision.name);
            }

            Destroy(gameObject); // 击中后移除子弹
        }
    }
}