
using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AmbienceController ambienceController;
    public bool isInterior;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isInterior)
            ambienceController.PlayInterior();
        else
            ambienceController.PlayCity();
    }
}
