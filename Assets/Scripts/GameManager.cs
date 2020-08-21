using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public GameObject gameOverUI;
    public Vector2 lastCheckpointPos;
    public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        score = 0;
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public static void GameOver(bool dead)
    {
        if (dead)
        {
            instance.gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
