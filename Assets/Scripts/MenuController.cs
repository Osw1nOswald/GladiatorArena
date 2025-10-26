using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playSceneName;

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(playSceneName))
            SceneManager.LoadScene(playSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
