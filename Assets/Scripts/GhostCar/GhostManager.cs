using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class GhostManager : MonoBehaviour
{
    public float timeBetweenSamples = 0.25f;
    public GhostLapData bestLapSO;              // Scriptable object that will contain the ghost data
    public GameObject carToRecord;
    public GameObject carToPlay;

    // RECORD VARIABLES
    private bool shouldRecord = false;
    private float totalRecordedTime = 0.0f;
    private float currenttimeBetweenSamples = 0.0f;

    // REPLAY VARIABLES
    private bool shouldPlay = false;
    private float totalPlayedTime = 0.0f;
    private float currenttimeBetweenPlaySamples = 0.0f;
    private int currentSampleToPlay = 0;

    // POSITIONS/ROTATIONS
    private Vector3 lastSamplePosition = Vector3.zero;
    private Quaternion lastSampleRotation = Quaternion.identity;
    private Vector3 nextPosition;
    private Quaternion nextRotation;



    #region RECORD GHOST DATA
    void StartRecording()
    {
        Debug.Log("START RECORDING");
        shouldRecord = true;
        shouldPlay = false;

        // Seteamos los valores iniciales
        totalRecordedTime = 0;
        currenttimeBetweenSamples = 0;

        // Limpiamos el scriptable object
        bestLapSO.Reset();
    }

    void StopRecording()
    {
        Debug.Log("STOP RECORDING");
        shouldRecord = false;
    }
    #endregion

    #region PLAY GHOST DATA
    void StartPlaying()
    {
        Debug.Log("START PLAYING");
        shouldPlay = true;
        shouldRecord = false;

        // Seteamos los valores iniciales
        totalPlayedTime = 0;
        currentSampleToPlay = 0;
        currenttimeBetweenPlaySamples = 0;

        // Desactivamos el control del coche
        carToPlay.GetComponent<CarController>().enabled = false;
        carToPlay.GetComponent<CarUserControl>().enabled = false;
    }

    void StopPlaying()
    {
        Debug.Log("STOP PLAYING");
        shouldPlay = false;

        // Devolvemos el control al coche por si fuera necesario (opcional)
        carToPlay.GetComponent<CarController>().enabled = true;
        carToPlay.GetComponent<CarUserControl>().enabled = true;

    }
    #endregion

    private void Update()
    {
        HandleTestActionInputs();

        if (shouldRecord)
        {
            // A cada frame incrementamos el tiempo transcurrido 
            totalRecordedTime += Time.deltaTime;
            currenttimeBetweenSamples += Time.deltaTime;

            // Si el tiempo transcurrido es mayor que el tiempo de muestreo
            if (currenttimeBetweenSamples >= timeBetweenSamples)
            {
                // Guardamos la información para el fantasma
                bestLapSO.AddNewData(carToRecord.transform);
                // Dejamos el tiempo extra entre una muestra y otra
                currenttimeBetweenSamples -= timeBetweenSamples;
            }
        }
        else if (shouldPlay)
        {
            // A cada frame incrementamos el tiempo transcurrido 
            totalPlayedTime += Time.deltaTime;
            currenttimeBetweenPlaySamples += Time.deltaTime;

            // Si el tiempo transcurrido es mayor que el tiempo de muestreo
            if (currenttimeBetweenPlaySamples >= timeBetweenSamples)
            {
                // De cara a interpolar de una manera fluida la posición del coche entre una muestra y otra,
                // guardamos la posición y la rotación de la anterior muestra
                lastSamplePosition = nextPosition;
                lastSampleRotation = nextRotation;

                // Cogemos los datos del scriptable object
                bestLapSO.GetDataAt(currentSampleToPlay, out nextPosition, out nextRotation);

                // Dejamos el tiempo extra entre una muestra y otra
                currenttimeBetweenPlaySamples -= timeBetweenSamples;

                // Incrementamos el contador de muestras
                currentSampleToPlay++;
            }

            // De cara a crear una interpolación suave entre la posición y rotación entre una muestra y la otra, 
            // calculamos a nivel de tiempo entre muestras el porcentaje en el que nos encontramos
            float percentageBetweenFrames = currenttimeBetweenPlaySamples / timeBetweenSamples;
            Debug.Log(percentageBetweenFrames);

            // Aplicamos un lerp entre las posiciones y rotaciones de la muestra anterior y la siguiente según el procentaje actual.
            carToPlay.transform.position = Vector3.Slerp(lastSamplePosition, nextPosition, percentageBetweenFrames);
            carToPlay.transform.rotation = Quaternion.Slerp(lastSampleRotation, nextRotation, percentageBetweenFrames);
        }
    }


    void HandleTestActionInputs()
    {
        // START/STOP RECORDING
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (shouldRecord)
                StopRecording();
            else
                StartRecording();
        }

        // PLAY RECORDED LAP
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shouldPlay)
                StopPlaying();
            else
                StartPlaying();
        }

        // RESET
        if (Input.GetKeyDown(KeyCode.Delete))
            bestLapSO.Reset();
    }

}
