using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning = false;
    public TMP_Text timeText;

    public void StartTimer()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    public void SetTime(float time)
    {
        timeRemaining = time;
    }

     void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("SHWABOOOOOOOOOOOOOOOOOOOM");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        // Add 1 to ensure it doesn't show 0 until the time is actually up
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay); 
        float milliseconds = Mathf.FloorToInt((timeToDisplay % 1) * 1000);

        // Formats string as "00.000"
        timeText.text = string.Format("{0:00}.{1:000}", seconds, milliseconds);
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }
}
