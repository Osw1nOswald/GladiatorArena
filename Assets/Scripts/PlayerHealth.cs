using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("������ �� �������")]
    public HealthBar healthBar; // ������ �� ���� HealthBar

    [Header("��������� ��������")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    private void Start()
    {
        // ������������� ��������
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxHealth = maxHealth;
            healthBar.health = currentHealth;
        }
    }

    // ����� ��������� �����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.health = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("����� ����!");
        // ����� ����� �������� ����� ������, �������� � �.�.
    }
}
