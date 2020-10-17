using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{

    private float _bestTime;
    public GameData()
    {
    }

    public void initData()
    {
        _bestTime = 0;
    }

    // Properties
    public float BestTime
    {
        get
        {
            return _bestTime;
        }
        set
        {
            _bestTime = value;
        }
    }

}