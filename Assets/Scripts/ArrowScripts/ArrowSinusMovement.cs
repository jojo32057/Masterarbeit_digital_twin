using UnityEngine;
using System.Collections;

public class ArrowSinusMovement : MonoBehaviour
{
    public float amplitude = 0.5f; // arrow height for sinus movement
    public float frequency = 3f; // arrow pace
    public int cycles = 1; // movement cycles 

    private Vector3 startPosition;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position; // define startposition
    }

    public void StartMoving()
    {
        if (!isMoving) // only one coroutine for multiple click events 
        {
            startPosition = transform.position; // update startPosition
            StartCoroutine(MoveInSinusoidalPattern());
        }
    }

    IEnumerator MoveInSinusoidalPattern()
    {
        isMoving = true;
        float timer = 0;
        int completedCycles = 0;

        while (completedCycles < cycles)
        {
            // Sinus movement calculation
            float height = Mathf.Sin(timer * frequency) * amplitude;
            transform.position = startPosition + new Vector3(0, height, 0);

            timer += Time.deltaTime;
            if (timer > 2 * Mathf.PI) // one cycle
            {
                timer = 0;
                completedCycles++;
            }
            yield return null;
        }

        isMoving = false;
    }
}