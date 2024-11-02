using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timerscript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float time;
    void Update()
    {
        time += Time.deltaTime;
        //calculates the minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        // Update the timer text to show minutes and seconds in "MM:SS" format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
