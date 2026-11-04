using UnityEngine;

public class GoldenOrbInteractable : Interactable
{
    public GameObject inventoryIcon;

    public GameObject platformPrefab;
    public Transform platformSpawnPoint;
    public TMPro.TMP_Text objectiveText;
    public override void Interact()
    {
        Debug.Log("Se ejecutó Interact de la reliquia");
        GameManager.Instance.hasGoldenOrb = true;
        GameManager.Instance.ActivateAlarm();
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