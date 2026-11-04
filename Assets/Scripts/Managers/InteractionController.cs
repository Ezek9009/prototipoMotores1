using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    public float interactionRange = 5f;

    public GameObject interactionText;
    public Camera playerCamera;
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

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, interactionRange);
        foreach (RaycastHit hit in hits)
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                break;
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