using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ButtonController : MonoBehaviour
{

    public GameObject prefabToSpawn;
    GameObject targetGameObject;
    public GameObject LogController;
    public float heightOffset = 1.0f;
    public float movementHeight = 1.0f;
    public float movementDuration = 1.0f;
    Vector3 newPosition;





    // Note: Function will not be used in this Use Case
    public void SpawnGameObject()
    {
        // define Target GameObject
        targetGameObject = GameObject.Find("Movable_Package");
        
        if (prefabToSpawn != null && targetGameObject != null)
        {
            Vector3 spawnPosition = targetGameObject.transform.position + new Vector3(0, heightOffset, 0);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);  


        } else {
            Debug.Log("noPrefab or TargetObject in SpawnGameObject");
        }

    }



    public void MoveGameObject(Button button)
    {


        // access to text component
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        // check if it exists
        if (buttonText != null)
        {

            // find GameObject "Arrow"
            GameObject arrowGameObject = GameObject.Find("Arrow");

            if (arrowGameObject != null)
            {
                // actives arrow if it is inaktiv
                if (!arrowGameObject.activeSelf)
                {
                    arrowGameObject.SetActive(true);
                }


                // calculate new position for arrow, based on the sensorID which is extracted from JSON Log 

                if (ExtractSensorId(buttonText.text) == "Robot_Vibration_Sensor" || ExtractSensorId(buttonText.text) == "Robot_Pace_Sensor")
                {
                    Debug.Log("RobotArmMesh should be called");
                    newPosition = GameObject.Find("RobotArmMesh").transform.position + new Vector3(0, heightOffset, 0);

                } 
                else if (ExtractSensorId(buttonText.text) == "Robot_Status_Sensor") 
                {
                    newPosition = GameObject.Find("RobotControllerScreen").transform.position + new Vector3(0, heightOffset, 0);
                } 
                else 
                {
                    // calculate new position for arrow, based on the sensorID which is extracted from JSON Log 
                    newPosition = GameObject.Find(ExtractSensorId(buttonText.text)).transform.position + new Vector3(0, heightOffset, 0);
                }

                // set new arrow position 
                arrowGameObject.transform.position = newPosition;

                // moves Arrow up and down 
                arrowGameObject.GetComponent<ArrowSinusMovement>().StartMoving();
            }
            else
            {
                // Error Handling 
                Debug.LogError("GameObject 'Arrow' not found!");
            }
        }
        else 
        {
            Debug.LogError("No Text in Log");
        }
    }


    // returns SensorID to calculate position of Arrow GameObject, when a Click-Event occurs 
    public string ExtractSensorId(string logJson)
        {
            // Parsen des JSON-Strings in ein LogData-Objekt
            LogData log = JsonUtility.FromJson<LogData>(logJson);

            string sensorId = log.sensor_id;

            return sensorId; 
        }


}
