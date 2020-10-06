using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GameplayManager : Singleton<GameplayManager>
{
    [Header("HUD")]
    public Text CountdownText;

    // Lap timers
    
    public Text Lap1TimerText;
    public Text Lap2TimerText;
    public Text Lap3TimerText;
    private Timer timer;

    private float[] _finalLapTimes;

    public CarController m_CarController; // Reference to car we are controlling

    

    void Start()
    {
        timer = GetComponentInChildren<Timer>();
        _finalLapTimes = new float[3];

        m_CarController.FullTorqueOverAllWheels = 0;
        StartCoroutine(Countdown(3));
    }

    void Update()
    {

    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            // Update CountdownText
            CountdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        // Countdown finished
        CountdownText.text = "GO!";
        // Unlock car
        m_CarController.FullTorqueOverAllWheels = 2500;
        yield return new WaitForSeconds(1);
        CountdownText.text = "";
        StartGame();
    }

    void StartGame()
    {
        
    }

    public void nextLap(int lapNumber)
    {
        switch (lapNumber)
        {
            case 1:
                timer.TimerText = Lap1TimerText;
                this.GetComponent<GhostManager>().StartRecording();
                break;
            case 2:
                _finalLapTimes[0] = timer.ActualLapTime;
                timer.TimerText = Lap2TimerText;
                timer.ResetTimer();
                this.GetComponent<GhostManager>().StartPlaying();
                break;
            case 3:
                _finalLapTimes[1] = timer.ActualLapTime;
                timer.TimerText = Lap3TimerText;
                timer.ResetTimer();
                break;
            case 4:
                // End race
                _finalLapTimes[2] = timer.ActualLapTime;
                timer.StopTimer();
                Debug.Log("Race END!!!");
                break;
        }
        if (lapNumber <= 3)
        {
            timer.StartTimer();
        }
    }
}
