using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink : MonoBehaviour
{
    private bool isBlinking = false;
    private Color originalColor;
    private MeshRenderer meshRenderer;
    private float blinkDuration = 0.5f; 

    private void Start()
    {
        // access MeshRenderer and original color
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isBlinking)
            {
                StartBlinking();
            }
            else
            {
                StopBlinking();
            }
        }
    }

    private void StartBlinking()
    {
        isBlinking = true;
        StartCoroutine(BlinkCoroutine());
    }

    private void StopBlinking()
    {
        isBlinking = false;
        StopAllCoroutines();
        meshRenderer.material.color = originalColor;
    }

    IEnumerator BlinkCoroutine()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Color startColor = renderer.material.color;
        Color endColor = Color.red;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / blinkDuration;
            renderer.material.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / blinkDuration;
            renderer.material.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }

        renderer.material.color = startColor;
        isBlinking = false;
    }
}