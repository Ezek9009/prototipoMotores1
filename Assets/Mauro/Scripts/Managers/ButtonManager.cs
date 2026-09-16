using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}