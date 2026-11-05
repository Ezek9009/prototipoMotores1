using UnityEngine;
using System.Collections.Generic;

public class PlayerDetectionAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip detectionSigh;

    private HashSet<Object> activeDetectors = new HashSet<Object>();

    public void OnPlayerDetected(Object detector)
    {
        if (activeDetectors.Contains(detector))
            return;

        activeDetectors.Add(detector);

        if (activeDetectors.Count == 1)
        {
            if (audioSource != null && detectionSigh != null)
                audioSource.PlayOneShot(detectionSigh);
        }
    }

    public void OnPlayerLost(Object detector)
    {
        activeDetectors.Remove(detector);
    }
}