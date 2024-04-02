using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{

    public GameObject Panel; 


    private void Start()
    {
        if(Panel != null && !Panel.activeSelf) 
        {
            Panel.SetActive(true); 
        }    
    }


    // closes/deactivates panel
    public void DeactivatePanel()
    {
        if(Panel != null) 
        {
            Panel.SetActive(false); 
        }
    }

}
