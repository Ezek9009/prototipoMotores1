using UnityEngine;

public class GoldenOrbInteractable : Interactable
{
    public GameObject inventoryIcon;

    public GameObject platformPrefab;
    public Transform platformSpawnPoint;
    public TMPro.TMP_Text objectiveText;
    public AudioClip pickupSound;
    public override void Interact()
    {
        StartCoroutine(PickupSequence());
    }

    private System.Collections.IEnumerator PickupSequence()
    {
        Debug.Log("Se ejecutó Interact de la reliquia");

        GameManager.Instance.hasGoldenOrb = true;

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            yield return new WaitForSeconds(1f);
        }

        GameManager.Instance.ActivateAlarm();

        if (inventoryIcon != null)
            inventoryIcon.SetActive(true);

        if (platformPrefab != null && platformSpawnPoint != null)
        {
            Instantiate(platformPrefab, platformSpawnPoint.position, platformSpawnPoint.rotation);
        }

        if (objectiveText != null)
        {
            objectiveText.text = "¡Ve a la camioneta!";
        }

        Destroy(gameObject);
    }
}