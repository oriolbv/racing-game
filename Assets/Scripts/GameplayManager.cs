using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GameplayManager : ExtendedBehaviour
{
    [Header("HUD")]
    public Text CountdownText;

    // Lap timers
    
    public Text Lap1TimerText;
    public Text Lap2TimerText;
    public Text Lap3TimerText;
    private Timer timer;

    private float[] _finalLapTimes;
    private float _bestTime;


    public CarController m_CarController; // Reference to car we are controlling

    void Start()
    {
        timer = GetComponentInChildren<Timer>();
        _finalLapTimes = new float[3];
        _bestTime = 0;
        GameData.Instance.initData();

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
                _finalLapTimes[0] = timer.ActualLapTime;
                _bestTime = _finalLapTimes[0];
                timer.TimerText = Lap2TimerText;
                timer.ResetTimer();
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
                EndGame();
                break;
        }
        if (lapNumber <= 3)
        {
            timer.StartTimer();
        }
    }

    private void EndGame()
    {
        foreach(float laptime in _finalLapTimes)
        {
            if (laptime < _bestTime)
            {
                _bestTime = laptime;
            }
        }

        GameData.Instance.BestTime = _bestTime;
        SceneManager.LoadScene("EndScene");
    }

}
