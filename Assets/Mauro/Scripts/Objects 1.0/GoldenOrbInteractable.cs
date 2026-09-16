using UnityEditor.Build.Content;
using UnityEngine;

public class GoldenOrbInteractable : Interactable
{
    public GameObject inventoryIcon;
    public override void Interact()
    {
        GameManager.Instance.hasGoldenOrb=true;
        inventoryIcon.SetActive(true);
        Destroy(gameObject);
    }
}
