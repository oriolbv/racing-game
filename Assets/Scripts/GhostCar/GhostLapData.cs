using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GhostLapData : ScriptableObject
{
    List<Vector3> carPositions;
    List<Quaternion> carRotations;


    public void Awake()
    {
        Debug.Log("HOOOOOOOOLA");
        carPositions = new List<Vector3>();
        carRotations = new List<Quaternion>();
    }
    public void AddNewData(Transform transform)
    {
        carPositions.Add(transform.position);
        carRotations.Add(transform.rotation);
        //Debug.Log("ADDED - " + carPositions.Count + ": Pos (" + transform.position + ") - Rot (" + transform.rotation.eulerAngles + ").");
    }

    public void GetDataAt(int sample, out Vector3 position, out Quaternion rotation)
    {
        position = carPositions[sample];
        rotation = carRotations[sample];
        //Debug.Log("PLAYED - " + sample + ": Pos (" + position + ") - Rot (" + rotation.eulerAngles + ").");
    }

    public void Reset()
    {
        Debug.Log("RESET");
        carPositions.Clear();
        carRotations.Clear();
    }
}
