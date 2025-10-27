using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [Header("Настройки урона")]
    public Collider swordCollider;
    public float damage = 25f;

    [Header("Звуки атаки")]
    public AudioSource audioSource;
    public AudioClip hitSound;   // звук попадания
    public AudioClip missSound;  // звук в пустоту

    private bool canDamage = false;
    private bool hasHit = false;

    private void Start()
    {
        if (swordCollider != null)
            swordCollider.enabled = false;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage || hasHit) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            hasHit = true;

            // ► Проигрываем звук попадания
            if (audioSource != null && hitSound != null)
                audioSource.PlayOneShot(hitSound);

            Debug.Log($"Меч попал во {other.name}, нанесено {damage} урона!");
        }
    }

    // Вызывается анимацией — начало окна урона
    public void EnableDamage()
    {
        canDamage = true;
        hasHit = false;
        if (swordCollider != null)
            swordCollider.enabled = true;
        Debug.Log("Урон включён");
    }

    // Вызывается анимацией — конец окна урона
    public void DisableDamage()
    {
        canDamage = false;

        // ► Если окно атаки закрылось, но попадания не было — звук "в пустоту"
        if (!hasHit && audioSource != null && missSound != null)
            audioSource.PlayOneShot(missSound);

        if (swordCollider != null)
            swordCollider.enabled = false;

        Debug.Log("Урон выключен");
    }
}
