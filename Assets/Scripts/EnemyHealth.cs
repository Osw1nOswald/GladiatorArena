using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона! ќсталось {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} погиб!");
        Destroy(gameObject); // пока просто удал€ем
    }
}
