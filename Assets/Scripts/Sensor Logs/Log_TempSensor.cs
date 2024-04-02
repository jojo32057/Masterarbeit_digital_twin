using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // Für CultureInfo.InvariantCulture


using UnityEngine.UI;
using TMPro; // TextMeshPro


public class Log_TempSensor : MonoBehaviour


{
    public float IntervalSeconds = 5.0f; // Zeitintervall in Sekunden
    private float nextActionTime = 0.0f;
    private PopulateScrollView populateScrollViewScript;
    public TextMeshProUGUI tempText; 
    public bool simulationStarted = false; 

    int maxDataCounter = 0; 





    void Update()
    {
        if (Time.time > nextActionTime && maxDataCounter < 10 && simulationStarted) 
        {
            nextActionTime += IntervalSeconds;
            GenerateLogData();

            maxDataCounter = maxDataCounter + 1; 
        }
    }


    public void startTempSensor ()
    {
        simulationStarted = true; 
        nextActionTime = Time.time + IntervalSeconds;
    }

    void GenerateLogData()
    {   
        float temperature = GetTemperature();
        string timestamp = DateTime.UtcNow.ToString("o");
        string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"{gameObject.name}\", \"data_type\": \"temperature\", \"value\": {temperature.ToString(CultureInfo.InvariantCulture)}}}";

        UpdateText(jsonLog);
        UpdateTemperatureText(temperature);
    }

    public void UpdateText(string message)
    {

        {
        // Findet das GameObject und holt sich die Referenz auf das PopulateScrollView-Skript
        GameObject logController = GameObject.Find("Log_Controller");
        if (logController != null)
        {
            populateScrollViewScript = logController.GetComponent<PopulateScrollView>();
        }

        // Überprüfen Sie, ob das Skript gefunden wurde, bevor Sie darauf zugreifen
        if (populateScrollViewScript != null)
        {
            populateScrollViewScript.writeToScrollView(message);
        }
    }

    }


    

    float GetTemperature()
    {
        // Hier kannst du die Logik für die Ermittlung der Temperatur implementieren.
        // Zur Demonstration generiere ich einen zufälligen Wert zwischen 20 und 25 Grad Celsius.
        return UnityEngine.Random.Range(20.0f, 25.0f);
    }



    public void UpdateTemperatureText(float temperature) 
    {
        if (tempText != null)
        {
            float roundedTemp = Mathf.Round(temperature * 10f) / 10f;
            tempText.text = $"Temperature: {roundedTemp} °C";
        }
        else
        {
            Debug.LogError("Text component not found.");
        }
    }
}