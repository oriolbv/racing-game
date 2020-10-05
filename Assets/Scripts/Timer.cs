using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _timerText;
    private float _time = 0.0f;

    private bool _bStarted = false;


    // Update is called once per frame
    void Update()
    {
        if (_bStarted)
        {
            _time += Time.deltaTime;
            _timerText.text = "" + _time.ToString("f0");
        }
    }

    public void StartTimer()
    {
        _bStarted = true;
    }

    public void StopTimer()
    {
        _bStarted = false;
    }

    public void ResetTimer()
    {
        _time = 0;
    }

    #region Properties

    public Text TimerText
    {
        get
        {
            return _timerText;
        }
        set
        {
            _timerText = value;
        }
    }

    public float ActualLapTime
    {
        get
        {
            return _time;
        }
    }

    #endregion
}
