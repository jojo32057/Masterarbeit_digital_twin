using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // Für CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro

public class Log_Robo_Status : MonoBehaviour
{

    private PopulateScrollView populateScrollViewScript;
    string status = "offline";
    public GameObject roboArm; 


    public void SendStatusLog()
    {

        if (roboArm != null)
        {
            JohannesMasterarbeit.ArmCollider roboArmScript = roboArm.GetComponent<JohannesMasterarbeit.ArmCollider>();

            if (roboArmScript != null)
            {

                float speed = roboArmScript.speed;
                if (speed > 0){
                    status = "working"; 
                } else {
                    status = "offline";
                }
                string timestamp = DateTime.UtcNow.ToString("o");
                string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"Robot_Status_Sensor\", \"data_type\": \"status\", \"value\": \"{status}\"}}";

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
