using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class TogglePanel : MonoBehaviour
{
    
    public GameObject Panel; 
    private Vector3 originalSize; 

    // variables to update Speed of Settingspanel 
    public GameObject conveyor1; 
    public GameObject conveyor2; 
    public GameObject robotArm; 
    public TextMeshProUGUI text1; 
    public TextMeshProUGUI text2; 
    public TextMeshProUGUI text3; 


    // set scale and deactivate the panel at start 
    private void Start()
    {
        originalSize = Panel.transform.localScale; 
        if(Panel != null) 
        {
            Panel.SetActive(false); 
        }
    }

    // opens and closes panel, based on current status 
    public void OpenPanel()
    {
        if(Panel != null) 
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive); 
            if (Panel.transform.localScale != originalSize)
            {
                Panel.transform.localScale = originalSize;
            }


            // Update Pace when Settingspanel is opened 
            if (Panel.name == "SettingsPanel" && Panel.activeSelf)
            {
                Conveyor_Belt conveyorBeltScript1 = conveyor1.GetComponent<Conveyor_Belt>();
                Conveyor_Belt conveyorBeltScript2 = conveyor2.GetComponent<Conveyor_Belt>();
                JohannesMasterarbeit.ArmCollider robotScript = robotArm.GetComponent<JohannesMasterarbeit.ArmCollider>(); 


                float speed1 = conveyorBeltScript1.speed;
                float speed2 = conveyorBeltScript2.speed;
                float speed3 = robotScript.speed;

                text1.text = $"Current Pace: <b>{speed1}</b>";
                text2.text = $"Current Pace: <b>{speed2}</b>";
                text3.text = $"Current Pace: <b>{speed3}</b>";


            }
        }
    }


}
