using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "E - Interactuar";

    public virtual void Interact()
    {
        
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null )
        {
            objectRenderer.material.color = Color.red;
        }
    }
}