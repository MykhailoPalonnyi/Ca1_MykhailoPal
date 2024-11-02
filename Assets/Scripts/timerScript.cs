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
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
