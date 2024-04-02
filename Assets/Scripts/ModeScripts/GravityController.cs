using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
    }


    public void activateGravity ()
    {

        if (rb != null)
        {
            rb.useGravity = true;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
    }




    

}
