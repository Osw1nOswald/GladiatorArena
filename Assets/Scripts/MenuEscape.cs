using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEscape : MonoBehaviour
{
    void Update()
    {
        // Проверяем, нажата ли клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Загружаем сцену меню
            SceneManager.LoadScene("Main Menu");
        }
    }
}
