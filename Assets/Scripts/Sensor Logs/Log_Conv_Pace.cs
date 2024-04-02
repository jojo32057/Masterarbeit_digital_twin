using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; // Für CultureInfo.InvariantCulture
using UnityEngine.UI;
using TMPro; // TextMeshPro

public class Log_Conv_Pace : MonoBehaviour
{

    public GameObject conveyor; 
    private PopulateScrollView populateScrollViewScript;


    // variables to update Speed of Settingspanel 
    public TextMeshProUGUI settingsText; 


    public void IncreasePace ()
    {
        CreateConveyorPaceLog(1);
    }


    public void DecreasePace ()
    {
        CreateConveyorPaceLog(-1);
    }


    public void PrintPaceLog()
    {
        CreateConveyorPaceLog(0);
    }


    void CreateConveyorPaceLog(int i)
    {

        if (conveyor != null)
        {
            // try to get conveyor belt script 
            Conveyor_Belt conveyorBeltScript = conveyor.GetComponent<Conveyor_Belt>();

            if (conveyorBeltScript != null)
            {

                // set new value for speed dependend on the button 
                conveyorBeltScript.speed = conveyorBeltScript.speed + i;

                float speed = conveyorBeltScript.speed;
                string timestamp = DateTime.UtcNow.ToString("o");
                string jsonLog = $"{{\"timestamp\": \"{timestamp}\", \"sensor_id\": \"{gameObject.name}\", \"data_type\": \"speed\", \"value\": {speed.ToString(CultureInfo.InvariantCulture)}}}";

                UpdateText(jsonLog);


                // Update text in Simulation Mode Settings 
                settingsText.text = $"Current Pace: <b>{speed}</b>";
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