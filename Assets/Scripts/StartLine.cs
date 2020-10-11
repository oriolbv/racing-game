using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : Singleton<StartLine>
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
            GameplayManager.Instance.GetComponent<GhostManager>().StopRecording();
            // GameplayManager.Instance.GetComponent<GhostManager>().StartRecording();
            _actualLap += 1;
            GameplayManager.Instance.nextLap(_actualLap);
            if (_actualLap >= 2) 
            {
                GameplayManager.Instance.GetComponent<GhostManager>().StopPlaying();
                GameplayManager.Instance.GetComponent<GhostManager>().StartPlaying();
            }
        } 
        else if (other.gameObject.name.Equals("GhostCar"))
        {
            GameplayManager.Instance.GetComponent<GhostManager>().StopPlaying();
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
