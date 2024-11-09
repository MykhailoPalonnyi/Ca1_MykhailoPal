using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current level
    }
}
