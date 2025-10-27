using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Ragdoll")]
    public GameObject ragdollPrefab; // сюда перетащи свой префаб ragdoll
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
        Debug.Log($"{gameObject.name} получил {damage} урона! Осталось {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} погиб!");

        // Создаём ragdoll на месте врага
        if (ragdollPrefab != null)
        {
            GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            Destroy(ragdoll, ragdollLifetime);
        }

        // Добавляем очки и время
        GameManager.Instance.AddScore(100);
        GameManager.Instance.AddTime(5f);
        GameManager.Instance.IncreaseCombo();

        Destroy(gameObject); // удаляем врага
    }
}
