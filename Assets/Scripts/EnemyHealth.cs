using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("��������� ��������")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Ragdoll")]
    public GameObject ragdollPrefab; // ���� �������� ���� ������ ragdoll
    public float ragdollLifetime = 5f;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} ������� {damage} �����! �������� {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} �����!");

        // ������ ragdoll �� ����� �����
        if (ragdollPrefab != null)
        {
            GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            Destroy(ragdoll, ragdollLifetime);
        }

        // ��������� ���� � �����
        GameManager.Instance.AddScore(100);
        GameManager.Instance.AddTime(5f);
        GameManager.Instance.IncreaseCombo();

        Destroy(gameObject); // ������� �����
    }
}
