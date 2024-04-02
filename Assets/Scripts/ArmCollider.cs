using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohannesMasterarbeit

{

    public class ArmCollider : MonoBehaviour
{

    public TargetBehavior currentPosition;
    public Transform endpoint;
    public GameObject robotArmTarget;  
    public float speed = 3; 
    private Collider currentObject;

    private bool hasCollided = false;
    private bool notArrivedEndpoint = true;

    void Awake () 
    {
        currentPosition = robotArmTarget.GetComponent<TargetBehavior>();
    }

    void OnTriggerStay(Collider other)
    {

        if (hasCollided) return;

        // move arm to package 
        currentPosition.current = Vector3.MoveTowards(currentPosition.current, other.transform.position, speed * Time.deltaTime);

        // add difference and tolerance of 0.1f to handle errors
        Vector3 difference = currentPosition.current - other.transform.position;

        // stop moving arm to endposition, if the endposition is reached
        if (difference.magnitude < 0.1f) {

            // assign currentObject to move it in Update Method
            currentObject = other; 
            hasCollided = true; 
        }

    }

    void Update()
    {
        if (hasCollided && notArrivedEndpoint) {

            // move arm to endposition
            currentPosition.current = Vector3.MoveTowards(currentPosition.current, endpoint.transform.position, speed * Time.deltaTime);

            // move package with arm until endposition is reached
            currentObject.transform.position = currentPosition.current; 

            // fix rotation error while moving package 
            currentObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            // add difference and tolerance of 0.1f to handle errors
            Vector3 difference = currentPosition.current - endpoint.transform.position;

            // stop moving arm to endposition, if the endposition is reached
            if (difference.magnitude < 0.1f) {
                notArrivedEndpoint = false; 
                
                // active rotation again 
                currentObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                RefreshStartCondition();
            }
        }
    }


    public void RefreshStartCondition ()
    {
        // makes robot arm ready for next GameObject to move 
        hasCollided = false;
        notArrivedEndpoint = true;
        currentObject = null;
    }
}





}

