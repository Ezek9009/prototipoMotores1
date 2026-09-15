using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    public float interactionRange = 5f;

    public GameObject interactionText;

    private Interactable currentInteractable;

    void Update()
    {
        FindInteractable();

        if (currentInteractable != null && Keyboard.current.eKey.wasPressedThisFrame)
           
        {
            currentInteractable.Interact();
        }
    }

    void FindInteractable()
    {
        currentInteractable = null;

        Ray ray = new Ray(transform.position, transform.forward);
          
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                
            if (interactable != null)
            {
                currentInteractable = interactable;
            }
        }

        if (currentInteractable != null)
        {
            interactionText.SetActive(true);

            interactionText.GetComponent<TMPro.TMP_Text>().text = currentInteractable.interactionMessage;
               
        }
        else
        {
            interactionText.SetActive(false);
        }
    }
}