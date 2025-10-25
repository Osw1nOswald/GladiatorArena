using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health;          // ������� ���������� ������
    public float maxHealth;       // ������������ ���������� ������
    public Image healthBar;       // ����� �������� (UI Image)

    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;  // ��� ������� �������� = �������������
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHealth;  // ����� �������� � ����������� �� ��������
    }
}
