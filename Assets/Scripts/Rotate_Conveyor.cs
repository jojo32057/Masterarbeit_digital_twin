using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Conveyor : MonoBehaviour
{

    // Spinning cylinder 
    float xRotationSpinner = 0f;
    float yRotationSpinner = 0f;
    float zRotationSpinner = -1f;

    public GameObject conveyorBeltInstance; // Define Gameobject 
    float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        conveyorBeltInstance = GameObject.FindWithTag("Conveyor"); // find the GameObject by tag 
        speedMultiplier = conveyorBeltInstance.GetComponent<Conveyor_Belt>().speed; // Access Speed of Conveyor to apply to wheel rotation
        // Debug.Log(speedMultiplier);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(xRotationSpinner, yRotationSpinner, zRotationSpinner * speedMultiplier);

    }
}
