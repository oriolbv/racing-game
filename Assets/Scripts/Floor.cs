using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Floor : MonoBehaviour
{
    public CarController m_CarController; // Reference to car we are controlling

    private bool _isInTerrain;

    // Start is called before the first frame update
    void Start()
    {
        _isInTerrain = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.layer != 9 && !_isInTerrain && collision.gameObject.tag != "Start")
        {
            Debug.Log("TERRAIN!");
            _isInTerrain = true;
            m_CarController.FullTorqueOverAllWheels = 500;
        }
        else if (collision.gameObject.layer == 9 && _isInTerrain)
        {
            Debug.Log("CIRCUIT!");
            _isInTerrain = false;
            m_CarController.FullTorqueOverAllWheels = 2500;
        }
    }
}
