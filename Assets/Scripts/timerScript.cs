using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timerscript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestTimeText;

    float time;
    public bool isLevelCompleted = false;
    void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (bestTime < float.MaxValue)
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60);
            int bestSeconds = Mathf.FloorToInt(bestTime % 60);

            bestTimeText.text = string.Format("{0:00}:{1:00}", bestMinutes, bestSeconds);
           

        }
        else
        {
            bestTimeText.text = "Best Time: --:--";
        }
    }
    void Update()
    {
        
        if (!isLevelCompleted) // Only update time if the level is not completed
        {
            time += Time.deltaTime;
        //calculates the minutes and seconds
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
        // Update the timer text to show minutes and seconds in "MM:SS" format
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    public void SaveBestTime()
    {
        if (time < PlayerPrefs.GetFloat("BestTime", float.MaxValue))
        {
            PlayerPrefs.SetFloat("BestTime", time);
            PlayerPrefs.Save();
        }
    }
    public void RestartLevel()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current level
    }

}
