using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGood : MonoBehaviour
{
    public void RestartGameGood()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
