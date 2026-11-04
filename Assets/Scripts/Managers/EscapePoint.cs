using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.StopAlarm();
            SceneManager.LoadScene("Victory");
        }
    }
}
