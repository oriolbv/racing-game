using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : ExtendedBehaviour
{

    private int _actualLap;
    private bool _ghostFinished;

    public GameplayManager gameplayManager;

    // Start is called before the first frame update
    void Start()
    {
        _actualLap = 0;
        _ghostFinished = false;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Car"))
        {
            if (_actualLap == 0)
            {
                gameplayManager.GetComponent<GhostManager>().StartRecording();
            }
            else if (_actualLap == 1) 
            {
                gameplayManager.GetComponent<GhostManager>().StartPlaying();
            }
            else
            {
                if (!_ghostFinished) 
                {
                    gameplayManager.GetComponent<GhostManager>().StopPlaying();
                    gameplayManager.GetComponent<GhostManager>().StopRecording();
                }
            }
            _actualLap += 1;
            gameplayManager.nextLap(_actualLap);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("GhostCar"))
        {
            gameplayManager.GetComponent<GhostManager>().StopPlaying();
            gameplayManager.GetComponent<GhostManager>().StopRecording();
            Destroy(other.gameObject);
            _ghostFinished = true;
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
