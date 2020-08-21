using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    int score;

    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        GetComponent<Text>().text = "你的最终得分：" + score;
    }
}
