using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkGameObject : MonoBehaviour
{

    public GameObject objectToHighlight;
    public GameObject networkPanel;
    public float blinkInterval = 0.5f;
    public int blinkCount = 3;

    private List<Color> originalColors = new List<Color>();
    private List<Renderer> renderers = new List<Renderer>();

    private Vector3 originalSize; 


    public void OnClick()
    {
        if (objectToHighlight != null)
        {
            StartCoroutine(BlinkObject());
        }
        else
        {
            Debug.LogError("Object to highlight is not assigned.");
        }

        if (networkPanel != null)
        {
            //networkPanel.SetActive(false);
            originalSize = networkPanel.transform.localScale; 
            networkPanel.transform.localScale = Vector3.zero;

        }
        else
        {
            Debug.LogError("NetworkPanel is not assigned.");
        }
    }

    IEnumerator BlinkObject()
    {
        FindAllRenderers(objectToHighlight);
        StoreOriginalColors();

        for (int i = 0; i < blinkCount; i++)
        {
            yield return StartCoroutine(TransitionColor(Color.red, blinkInterval / 1)); // change color here if necessary
            yield return StartCoroutine(TransitionColor(originalColors, blinkInterval / 1));
        }

        RestoreOriginalColors();
        networkPanel.transform.localScale = originalSize; 
        networkPanel.SetActive(false);

    }

    void FindAllRenderers(GameObject obj)
    {
        renderers.AddRange(obj.GetComponents<Renderer>());
        foreach (Transform child in obj.transform)
        {
            FindAllRenderers(child.gameObject);
        }
    }

    void StoreOriginalColors()
    {
        originalColors.Clear();
        foreach (Renderer renderer in renderers)
        {
            originalColors.Add(renderer.material.color);
        }
    }

    IEnumerator TransitionColor(Color targetColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = Color.Lerp(renderer.material.color, targetColor, time / duration);
            }
            time += Time.deltaTime;
            yield return null;
        }

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = targetColor;
        }
    }

    IEnumerator TransitionColor(List<Color> targetColors, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material.color = Color.Lerp(renderers[i].material.color, targetColors[i], time / duration);
            }
            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material.color = targetColors[i];
        }
    }

    void RestoreOriginalColors()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
    }
}
