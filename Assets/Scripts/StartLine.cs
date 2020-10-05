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
            Debug.Log("VOLTA!");
            _actualLap += 1;
            GameplayManager.Instance.nextLap(_actualLap);
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
