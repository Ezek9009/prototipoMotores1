using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    public AudioSource cityAmbience;
    public AudioSource interiorAmbience;

    private void Start()
    {
        PlayCity();
    }

    public void PlayCity()
    {
        interiorAmbience.Stop();

        if (!cityAmbience.isPlaying)
            cityAmbience.Play();
    }

    public void PlayInterior()
    {
        cityAmbience.Stop();

        if (!interiorAmbience.isPlaying)
            interiorAmbience.Play();
    }
}