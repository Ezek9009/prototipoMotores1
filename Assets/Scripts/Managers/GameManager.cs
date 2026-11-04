using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasGoldenOrb = false;
    public bool alarmActive = false;

    [SerializeField] private AudioSource alarmAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateAlarm()
    {
        if (alarmActive)
            return;

        alarmActive = true;
        Debug.Log("Alarma Activada");
        if (alarmAudio != null)
            alarmAudio.Play();
        else
        {
            Debug.LogWarning("No hay AudioSource activado");
        }
    }

    public void StopAlarm()
    {
        alarmActive = false;

        if (alarmAudio != null)
            alarmAudio.Stop();
    }
}