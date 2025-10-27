using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [Header("Настройки урона")]
    public Collider swordCollider; // перетащи сюда коллайдер меча
    public float damage = 25f;

    private bool canDamage = false;
    private bool hasHit = false;

    private void Start()
    {
        if (swordCollider != null)
            swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage || hasHit) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            hasHit = true;
            Debug.Log($"Меч попал во {other.name}, нанесено {damage} урона!");
        }
    }

    // 🔹 Эти методы вызываются анимацией
    public void EnableDamage()
    {
        canDamage = true;
        hasHit = false;
        if (swordCollider != null)
            swordCollider.enabled = true;
        Debug.Log("Урон включён");
    }

    public void DisableDamage()
    {
        canDamage = false;
        if (swordCollider != null)
            swordCollider.enabled = false;
        Debug.Log("Урон выключен");
    }
}
