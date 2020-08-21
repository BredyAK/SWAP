using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    
    void FixedUpdate()
    {
        if (player.transform.position.x > -55f && player.transform.position.x < 277f)
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10f);
        }
    }
}
