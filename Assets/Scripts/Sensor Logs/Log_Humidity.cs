using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro

public class Log_Humidity : MonoBehaviour
{
    public float IntervalSeconds = 5.0f; // in seconds
    private float nextActionTime = 0.0f;
    private PopulateScrollView populateScrollViewScript;
    public TextMeshProUGUI humText; 
    int maxDataCounter = 0;
    public bool simulationStarted = false; 


 
    void Update()
    {
        if (Time.time > nextActionTime && maxDataCounter < 10 && simulationStarted) 
        {
            nextActionTime += IntervalSeconds;
            GenerateLogData();
            maxDataCounter++;
        }
    }

    public void StartHumiditySensor ()
    {
        simulationStarted = true; 
        nextActionTime = Time.time + IntervalSeconds;
    }

    void GenerateLogData()
    {   
        float humidity = GetHumidity();
        string timestamp = DateTime.UtcNow.ToString("o");
        string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"{gameObject.name}\", \"data_type\": \"humidity\", \"value\": {humidity.ToString(CultureInfo.InvariantCulture)}}}";

        UpdateText(jsonLog);
        UpdateHumidityText(humidity);
    }

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

    float GetHumidity()
    {
        // Random Humidity between 40% and 60%
        return UnityEngine.Random.Range(40.0f, 60.0f);
    }


    public void UpdateHumidityText(float humidity) 
    {
        if (humText != null)
        {
            float roundedHum = Mathf.Round(humidity * 10f) / 10f;
            humText.text = $"Humidity: {roundedHum} %";
        }
        else
        {
            Debug.LogError("Text component not found.");
        }
    }
}