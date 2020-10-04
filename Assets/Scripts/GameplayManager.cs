using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GameplayManager : MonoBehaviour
{
    [Header("HUD")]
    public Text CountdownText;

    // Lap timers
    public Text Lap1TimerText;
    public Text Lap2TimerText;
    public Text Lap3TimerText;


    public CarController m_CarController; // Reference to car we are controlling

    
    void Start()
    {
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
        // UNLOCK CAR!
        
    }
}
