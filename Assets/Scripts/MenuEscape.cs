using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEscape : MonoBehaviour
{
    void Update()
    {
        // ���������, ������ �� ������� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ��������� ����� ����
            SceneManager.LoadScene("Main Menu");
        }
    }
}
