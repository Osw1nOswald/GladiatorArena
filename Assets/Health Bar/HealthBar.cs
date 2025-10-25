using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health;          // Текущее количество жизней
    public float maxHealth;       // Максимальное количество жизней
    public Image healthBar;       // Шкала здоровья (UI Image)

    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;  // При запуске здоровье = максимальному
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHealth;  // Шкала меняется в зависимости от здоровья
    }
}
