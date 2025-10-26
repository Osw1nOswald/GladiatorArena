using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ссылка на хелсбар")]
    public HealthBar healthBar; // Ссылка на твой HealthBar

    [Header("Настройки здоровья")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    private void Start()
    {
        // Инициализация хелсбара
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxHealth = maxHealth;
            healthBar.health = currentHealth;
        }
    }

    // Метод получения урона
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
        Debug.Log("Игрок умер!");
        // позже можно добавить экран смерти, анимацию и т.д.
    }
}
