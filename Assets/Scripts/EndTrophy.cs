using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrophy : MonoBehaviour
{
    GameManager gm;

    int score;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        PlayerPrefs.SetInt("Score", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Score", score);
            gm.lastCheckpointPos = new Vector2(-65f, -4f);
            if (score > 50000)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

            gm.score = 0;
        }
    }

    // GameText会将分数自动同步至此
    public void SyncScore(int playerScore)
    {
        score = playerScore;
    }
}
