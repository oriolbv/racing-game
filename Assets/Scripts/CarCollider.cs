using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarCollider : MonoBehaviour
{
    // Constants
    int CIRCUIT_LAYER = 9;


    public CarController m_CarController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(CIRCUIT_LAYER))
        {
            Debug.Log("ENTER CIRCUIT");
            m_CarController.Topspeed = 200;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(CIRCUIT_LAYER))
        {
            Debug.Log("EXIT CIRCUIT");
            m_CarController.Topspeed = 30;
        }
    }
}
