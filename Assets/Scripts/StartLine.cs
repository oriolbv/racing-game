﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : ExtendedBehaviour
{

    private int _actualLap;
    // Start is called before the first frame update
    void Start()
    {
        _actualLap = 0;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Car"))
        {
            if (_actualLap == 0)
            {
                GameplayManager.Instance.GetComponent<GhostManager>().StartRecording();
            }
            else if (_actualLap == 1) 
            {
                GameplayManager.Instance.GetComponent<GhostManager>().StartPlaying();
            }
            else
            {
                GameplayManager.Instance.GetComponent<GhostManager>().StopPlaying();
                GameplayManager.Instance.GetComponent<GhostManager>().StopRecording();
            }
            _actualLap += 1;
            GameplayManager.Instance.nextLap(_actualLap);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("GhostCar"))
        {
            GameplayManager.Instance.GetComponent<GhostManager>().StopPlaying();
            GameplayManager.Instance.GetComponent<GhostManager>().StopRecording();
            Destroy(other.gameObject);
        }
    }

    #region Properties

    public int ActualLap
    {
        get
        {
            return _actualLap;
        }
    }

    #endregion
}
