using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshStartposition : MonoBehaviour
{

    public GameObject target; 
    private Vector3 targetPosition; 


    void Start()
    {
        // safe position of Target in variable
        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }
    }

    // set position of Target to targetPosition when an OnClick Event occurs  
    public void ResetToStartPosition()
    {
        if (target != null)
        {
            target.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }

    }


}
