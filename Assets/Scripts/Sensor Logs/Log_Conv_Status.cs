using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // Für CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro

public class Log_Conv_Status : MonoBehaviour
{


    private PopulateScrollView populateScrollViewScript;
    string status = "offline";
    public GameObject conveyor; 


    public void SendStatusLog()
    {

        if (conveyor != null)
        {
            // Versuchen, das Conveyor_Belt Skript zu erhalten
            Conveyor_Belt conveyorBeltScript = conveyor.GetComponent<Conveyor_Belt>();

            if (conveyorBeltScript != null)
            {
                // Auslesen der Speed-Variable
                float speed = conveyorBeltScript.speed;
                if (speed > 0){
                    status = "working"; 
                } else {
                    status = "offline";
                }
                string timestamp = DateTime.UtcNow.ToString("o");
                string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"{gameObject.name}\", \"data_type\": \"status\", \"value\": \"{status}\"}}";

                UpdateText(jsonLog);

            }
            else
            {
                Debug.LogError("Conveyor_Belt script not found on Conveyor GameObject.");
            }
        }
        else
        {
            Debug.LogError("Conveyor GameObject not found.");
        }
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


}