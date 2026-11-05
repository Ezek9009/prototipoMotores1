
using UnityEngine;

public class RadioInteractable : Interactable
{
    public AudioSource radioAudio;

    private bool isPlaying = false;

    public override void Interact()
    {
        if (isPlaying)
        {
            radioAudio.Stop();
            isPlaying = false;
        }
        else
        {
            radioAudio.Play();
            isPlaying = true;
        }
    }
}
