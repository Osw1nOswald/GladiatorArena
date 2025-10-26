using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [Header("Настройки урона")]
    public float damage = 25f;
    private bool canDamage = false; // активен ли урон

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        // Проверяем, враг ли это
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"Меч попал во {other.name}, нанесено {damage} урона!");
            Debug.Log($"Trigger сработал на {other.name}");
        }
    }

    // Эти методы вызываются анимацией
    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}
