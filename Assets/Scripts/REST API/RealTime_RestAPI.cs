using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System;
using System.Globalization;
using TMPro; 



public class RealTime_RestAPI : MonoBehaviour
{

    // structure for the log data in c#, it only works with this structure 
    public class Logdata
    {
        public DateTime timestamp { get; set; }
        public string sensor_id { get; set; }
        public string data_type { get; set; }
        public object value { get; set; }
    }

    public class LogRoot
    {
        public int current_page { get; set; }
        public List<Logdata> data { get; set; }
    }


    private PopulateScrollView populateScrollViewScript;
    private HashSet<string> displayedLogs = new HashSet<string>(); // safe already displayed logs 
    public TextMeshProUGUI LoadLogText; 
    private string input = "http://localhost:3000/";



    // changes the standard URL to a specific new one to load log data 
    public void ReadStringURL (string s)
    {
        input = s; 
    }



    public void StartRealTimeGetRequest()
    {
        StartCoroutine(RealTimeGetRequest(input)); 
        LoadLogText.text = "Loading...";
    }


    IEnumerator RealTimeGetRequest (string uri) 
    {
        while (true) 
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                    webRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError($"Something went wrong: {webRequest.error}");
                }
                else if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    LogRoot logs = JsonConvert.DeserializeObject<LogRoot>(webRequest.downloadHandler.text);
                    ProcessLogs(logs.data);
                }
            }

            yield return new WaitForSeconds(1); // wait for 1 sec for new Request 
        }
    }

    public void CreateLog(Logdata log)
    {

        string message = $"{{\"timestamp\": \"{log.timestamp.ToString("o", CultureInfo.InvariantCulture)}\", \"sensor_id\": \"{log.sensor_id}\", \"data_type\": \"{log.data_type}\", \"value\": \"{log.value}\"}}";

        GameObject logController = GameObject.Find("Log_Controller");
        if (logController != null)
        {
            populateScrollViewScript = logController.GetComponent<PopulateScrollView>();
            if (populateScrollViewScript != null)
            {
                populateScrollViewScript.writeToScrollView(message);
            }
        }
    }




    void ProcessLogs(List<Logdata> logs)
    {
        foreach (var log in logs)
        {
            string logIdentifier = log.timestamp.ToString("o", CultureInfo.InvariantCulture) + log.sensor_id; // create identifier to detect duplicates
            if (!displayedLogs.Contains(logIdentifier)) // test logs for duplicates 
            {
                CreateLog(log); // creates the log in a displayable form for Canvas and updates ScrollView
                UpdateSensorValue(log); // manipulates the sensor based on importet value 
                displayedLogs.Add(logIdentifier); // adds the log to a list of already displayed logs
            }
        }
    }
    




    public void UpdateSensorValue(Logdata log)
    {
      
        switch (log.sensor_id)
        {
            case "PaceSensor_1":
                UpdateConveyorSpeed("Conveyor_1", log);
                break;

            case "PaceSensor_2":
                UpdateConveyorSpeed("Conveyor_2", log);
                break;

            case "Robot_Pace_Sensor":
                UpdateRobotSpeed("StartAreaRobotArm", log);
                break;

            case "StatusSensor_2":
                UpdateConveyorStatus("Conveyor_2", log);
                break;

            case "StatusSensor_1":
                UpdateConveyorStatus("Conveyor_1", log);
                break;

            case "Robot_Status_Sensor":
                UpdateRobotStatus("StartAreaRobotArm", log);
                break;

            case "VibrationSensor_2":
                // currently no actions planned
                break;

            case "VibrationSensor_1":
                // currently no actions planned
                break;

            case "Robot_Vibration_Sensor":
                // currently no actions planned
                break;

            case "HumiditySensor":
                UpdateHumidityPanel("Text_Hum", log);
                break;
            
            case "TempSensor":
                UpdateTempPanel("Text_Temp", log);
                break;

            default:
                Debug.LogError("Sensor_id " + log.sensor_id + " is not matching");
                break;
        }
    }

    private void UpdateConveyorSpeed(string conveyorName, Logdata log)
    {
        var conveyor = GameObject.Find(conveyorName);
        if (conveyor != null)
        {
            var conveyorBeltScript = conveyor.GetComponent<Conveyor_Belt>();
            if (conveyorBeltScript != null)
            {
                if (float.TryParse(log.value.ToString(), out float speedValue))
                {
                    conveyorBeltScript.speed = speedValue;
                }
                else
                {
                    Debug.LogError("Conversion Error with following value: " + log.value);
                }
            }
            else
            {
                Debug.LogError("Conveyor_Belt-Component not found in " + conveyor.name);
            }
        }
        else
        {
            Debug.LogError("GameObject '" + conveyorName + "' not found");
        }
    }




    private void UpdateRobotSpeed(string robotArmName, Logdata log)
    {
        var robotArm = GameObject.Find(robotArmName);
        if (robotArm != null)
        {
            var robotScript = robotArm.GetComponent<JohannesMasterarbeit.ArmCollider>();
            if (robotScript != null)
            {
                if (float.TryParse(log.value.ToString(), out float speedValue))
                {
                    robotScript.speed = speedValue;
                }
                else
                {
                    Debug.LogError("Conversion Error with following value: " + log.value);
                }
            }
            else
            {
                Debug.LogError("Robot-Component not found in " + robotArm.name);
            }
        }
        else
        {
            Debug.LogError("GameObject '" + robotArmName + "' not found");
        }
    }




    private void UpdateConveyorStatus(string conveyorName, Logdata log)
    {
        var conveyor = GameObject.Find(conveyorName);
        if (conveyor != null)
        {
            var conveyorBeltScript = conveyor.GetComponent<Conveyor_Belt>();
            if (conveyorBeltScript != null)
            {
                if (log.value.ToString().ToLower() == "offline")
                {
                    conveyorBeltScript.speed = 0;
                    Debug.Log(conveyorName + " speed set to 0 due to offline status.");
                }
                else
                {
                    Debug.Log(conveyorName + " remains at current speed: " + conveyorBeltScript.speed);
                }
            }
            else
            {
                Debug.LogError("Conveyor_Belt-Component not found in " + conveyor.name);
            }
        }
        else
        {
            Debug.LogError("GameObject '" + conveyorName + "' not found");
        }
    }





    private void UpdateRobotStatus(string robotArmName, Logdata log)
    {
        var robotArm = GameObject.Find(robotArmName);
        if (robotArm != null)
        {
            var robotScript = robotArm.GetComponent<JohannesMasterarbeit.ArmCollider>();
            if (robotScript != null)
            {
                if (log.value.ToString().ToLower() == "offline")
                {
                    robotScript.speed = 0;
                }
            }
            else
            {
                Debug.LogError("Robot-Component not found in " + robotArm.name);
            }
        }
        else
        {
        Debug.LogError("GameObject '" + robotArmName + "' not found");
        }
    }




    private void UpdateTempPanel (string tempTextName, Logdata log)
    {
        GameObject tempGameObject = GameObject.Find(tempTextName);
        if (tempGameObject != null)
        {
            TextMeshProUGUI tempTextComponent = tempGameObject.GetComponent<TextMeshProUGUI>();  
            if (tempTextComponent != null)
            {
                if (float.TryParse(log.value.ToString(), out float temperature))
                {
                    float roundedTemp = Mathf.Round(temperature * 10f) / 10f;
                    tempTextComponent.text = $"Temperature: {roundedTemp} °C";
                }
                else
                {
                    Debug.LogError("Conversion Error with following value: " + log.value);
                }
            }
            else
            {
                Debug.LogError("TextMeshPro component not found in " + tempTextName);
            }
        }
        else
        {
            Debug.LogError("GameObject '" + tempTextName + "' not found");
        }

    }





    private void UpdateHumidityPanel(string humidityTextName, Logdata log)
    {
        GameObject humidityGameObject = GameObject.Find(humidityTextName);
        if (humidityGameObject != null)
        {
            TextMeshProUGUI humidityTextComponent = humidityGameObject.GetComponent<TextMeshProUGUI>();
            if (humidityTextComponent != null)
            {
                if (float.TryParse(log.value.ToString(), out float humidity))
                {
                    float roundedHumidity = Mathf.Round(humidity * 10f) / 10f;
                    humidityTextComponent.text = $"Humidity: {roundedHumidity}%";
                }
                else
                {
                    Debug.LogError("Conversion Error with following value: " + log.value);
                }
            }
            else
            {
                Debug.LogError("TextMeshPro component not found in " + humidityTextName);
            }
        }
        else
        {
            Debug.LogError("GameObject '" + humidityTextName + "' not found");
        }
    }



}
