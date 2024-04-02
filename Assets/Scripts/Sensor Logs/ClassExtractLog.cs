using UnityEngine;

public class ClassExtractLog : MonoBehaviour
{
    public void ExtractSensorId(string logJson)
    {
        // Parsen des JSON-Strings in ein LogData-Objekt
        LogData log = JsonUtility.FromJson<LogData>(logJson);

        // Zugriff auf die sensor_id des geparsten Objekts
        string sensorId = log.sensor_id;

        // Ausgabe oder weitere Verwendung der sensor_id
        Debug.Log("Sensor ID: " + sensorId);
    }
}