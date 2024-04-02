using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; //  CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro

public class Log_VibrationSensor : MonoBehaviour
{
    public float IntervalSeconds = 5.0f; // in seconds
    private float nextActionTime = 0.0f;
    private PopulateScrollView populateScrollViewScript;
    int maxDataCounter = 0;
    public bool simulationStarted = false; 
    string jsonLog;



    void Update()
    {
        if (Time.time > nextActionTime && maxDataCounter < 10 && simulationStarted) 
        {
            nextActionTime += IntervalSeconds;
            GenerateLogData();
            maxDataCounter++;
        }
    }

    public void StartVibrationSensor ()
    {
        simulationStarted = true; 
        nextActionTime = Time.time + IntervalSeconds;
    }


    // generates log data and updates scrollview 
    void GenerateLogData()
    {   
        float vibrationLevel = GetVibrationLevel();
        string timestamp = DateTime.UtcNow.ToString("o");
        if (gameObject.name == "RobotArm")
        {
            jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"Robot_Vibration_Sensor\", \"data_type\": \"vibration\", \"value\": {vibrationLevel.ToString(CultureInfo.InvariantCulture)}}}";
        } else {
            jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"{gameObject.name}\", \"data_type\": \"vibration\", \"value\": {vibrationLevel.ToString(CultureInfo.InvariantCulture)}}}";
        }

        UpdateText(jsonLog);
    }


    // adds json log to scrollview 
    public void UpdateText(string message)
    {
        GameObject logController = GameObject.Find("Log_Controller");
        if (logController != null)
        {
            populateScrollViewScript = logController.GetComponent<PopulateScrollView>();
        }

        if (populateScrollViewScript != null)
        {
            populateScrollViewScript.writeToScrollView(message);
        }
    }


    // generates random vibration value in a certain scale 
    float GetVibrationLevel()
    {
        // Simulierte Vibrationswerte zwischen 0 und 10
        return UnityEngine.Random.Range(0.0f, 10.0f);
    }
}