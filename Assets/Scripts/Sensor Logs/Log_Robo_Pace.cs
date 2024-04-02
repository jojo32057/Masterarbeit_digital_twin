using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // Für CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro


public class Log_Robo_Pace : MonoBehaviour
{
    public GameObject roboArm; 
    private PopulateScrollView populateScrollViewScript;


    // variables to update Speed of Settingspanel 
    public TextMeshProUGUI text; 


    public void IncreaseRoboPace ()
    {
        CreateRoboPaceLog(1);
    }


    public void DecreaseRoboPace ()
    {
        CreateRoboPaceLog(-1);
    }


    public void PrintRoboPaceLog()
    {
        CreateRoboPaceLog(0);
    }


    void CreateRoboPaceLog(int i)
    {

        if (roboArm != null)
        {
            // try to get robotArm script 
            JohannesMasterarbeit.ArmCollider roboScript = roboArm.GetComponent<JohannesMasterarbeit.ArmCollider>();

            if (roboScript != null)
            {

                // set new value for speed dependend on the button 
                roboScript.speed = roboScript.speed + i;

                float speed = roboScript.speed;
                string timestamp = DateTime.UtcNow.ToString("o");
                string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"Robot_Pace_Sensor\", \"data_type\": \"speed\", \"value\": {speed.ToString(CultureInfo.InvariantCulture)}}}";

                UpdateText(jsonLog);



                // Update text in Simulation Mode Settings 
                text.text = $"Current Pace: <b>{speed}</b>";

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
