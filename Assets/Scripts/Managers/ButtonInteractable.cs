using UnityEngine;

public class ButtonInteractable : Interactable
{
    public MovingPlatformInteractable platform;
    public override void Interact()
    {
        Debug.Log("Boton Interactuado");
        platform.ActivatePlatform();
    }
}
