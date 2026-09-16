using UnityEngine;

public class GoldenOrbInteractable : Interactable
{
    public GameObject inventoryIcon;

    public GameObject platformPrefab;
    public Transform platformSpawnPoint;
    public TMPro.TMP_Text objectiveText;
    public override void Interact()
    {
        GameManager.Instance.hasGoldenOrb = true;
        if (inventoryIcon != null)
            inventoryIcon.SetActive(true);
        
        if (platformPrefab != null && platformSpawnPoint != null)
        {
            Instantiate(platformPrefab, platformSpawnPoint.position,platformSpawnPoint.rotation);
        }
        if (objectiveText != null)
        {
            objectiveText.text = "¡Ve a la plataforma!";
        }
    
        Destroy(gameObject);
    }
}