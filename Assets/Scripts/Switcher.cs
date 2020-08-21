using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
{
    public Text selectionText;
    public Transform firePoint;
    public GameObject bulletPrefab;

    AudioSource audioFire;

    float antilagTimer = 0;

    void Start()
    {
        audioFire = GameObject.Find("AudioPlayerFire").GetComponent<AudioSource>();
    }

    void Update()
    {
        antilagTimer += Time.deltaTime;
        if (antilagTimer > 0.2 && Input.GetKeyDown(KeyCode.Z)) // 数字表示每发子弹射击最短时间间隔
        {
            Shoot();
            audioFire.Play();

            antilagTimer = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
