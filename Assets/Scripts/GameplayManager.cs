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

    private int _finalLapTimes;

    public CarController m_CarController; // Reference to car we are controlling

    

    void Start()
    {
        timer = GetComponentInChildren<Timer>();


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
                break;
            case 2:
                timer.TimerText = Lap2TimerText;
                break;
            case 3:
                timer.TimerText = Lap3TimerText;
                break;
        }
        timer.StartTimer();

    }
}
