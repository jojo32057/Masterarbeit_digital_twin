using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    public GameObject settingsButton; 
    public GameObject settingsPanel; 
    public GameObject buttonStartInvestigationMode; 
    public GameObject buttonStartSimulationMode; 
    public GameObject buttonStartRealTimeMode;



    // Start is called before the first frame update
    void Start()
    {
        DeactivateGameObject(settingsPanel); 
        DeactivateGameObject(settingsButton);
        DeactivateGameObject(buttonStartInvestigationMode);
        DeactivateGameObject(buttonStartSimulationMode);
        DeactivateGameObject(buttonStartRealTimeMode);

    }


    public void ActivateSimulationMode()
    {
        ActivateGameObject(settingsButton);
        ActivateGameObject(buttonStartSimulationMode);
    }


    public void ActivateInvestigationMode()
    {
        ActivateGameObject(buttonStartInvestigationMode);
    }


    public void ActivateRealTimeMode()
    {
        ActivateGameObject(buttonStartRealTimeMode);
    }



    public void ActivateGameObject(GameObject obj)
    {
        if (obj != null && !obj.activeSelf)
        {
            // activates the GameObject
            obj.SetActive(true);
        }
        else
        {
            Debug.Log("Object to activate is either not assigned or already active.");
        }
    }


    public void DeactivateGameObject (GameObject obj)
    {
        if (obj != null && obj.activeSelf)
        {
            // deactivates the GameObject
            obj.SetActive(false);
        }
        else
        {
            Debug.Log("Object to deactivate is either not assigned or already inactive.");
        }
    }
}


