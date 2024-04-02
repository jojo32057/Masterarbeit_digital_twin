using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{

    public GameObject targetObject; 
    private MeshRenderer meshRenderer; // MeshRenderer reference
    public float heightOffset = 1.0f;
    public float movementHeight = 1.0f;
    public float movementDuration = 1.0f;



    void Start()
    {
        // Get the MeshRenderer component from this gameObject
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        // Make sure the MeshRenderer is disabled at start
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
        else
        {
            Debug.LogError("MeshRenderer component missing!");
        }

        // Positioniert das "Arrow" über dem targetObject
        if (targetObject != null)
        {
            Vector3 newPosition = targetObject.transform.position + new Vector3(0, heightOffset, 0);
            transform.position = newPosition;

            // Startet die Sinuskurvenbewegung Coroutine
            StartCoroutine(MoveInSinusoidalPattern());
        }
        else
        {
            Debug.LogError("TargetObject not assigned!");
        }
    }




    public void StartMovingWithTarget()
    {

        targetObject = GameObject.Find("Arrow(Clone)");

        // Update the position based on the new target
        if (targetObject != null)
        {
            Vector3 newPosition = targetObject.transform.position + new Vector3(0, heightOffset, 0);
            transform.position = newPosition;

        }
        else
        {
            Debug.LogError("New target object not assigned!");
        }
    }





    public IEnumerator MoveInSinusoidalPattern()
    {
        Vector3 basePosition = transform.position;
        float timer = 0;

        meshRenderer.enabled = true;

        while (timer <= movementDuration * 6) // 3 cycles up and down
        {
            float height = Mathf.Sin(timer / movementDuration * Mathf.PI) * movementHeight;
            transform.position = basePosition + new Vector3(0, height, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        // Disable MeshRenderer 
        if (meshRenderer != null)
        {
            // meshRenderer.enabled = false;

            yield return new WaitForSeconds(1); 

            meshRenderer.enabled = false;
        }
    }

}
