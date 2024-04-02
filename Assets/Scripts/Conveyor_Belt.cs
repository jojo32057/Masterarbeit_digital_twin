using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Belt : MonoBehaviour
{


    public GameObject belt; 
    public Transform endpoint; 
    public float speed = 1; 
    

    // when collision, this method will move the gameObject to the endpoint with a speed 
    void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime); // Quelle: https://www.youtube.com/watch?v=1eoR1-65ZI0

    }


}
