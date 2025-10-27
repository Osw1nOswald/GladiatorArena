using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI элементы")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public CountdownTimer timer;

    private int score = 0;
    private int combo = 0;
    private float comboTimer = 0f;
    private const float comboTimeout = 4f; // 4 сек на следующее убийство для комбо

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (combo > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                combo = 0;
                UpdateComboText();
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount * Mathf.Max(1, combo); // множитель комбо
        scoreText.text = "Score: " + score;
    }

    public void AddTime(float seconds)
    {
        if (timer != null)
            timer.totalTime += seconds;
    }

    public void IncreaseCombo()
    {
        combo++;
        comboTimer = comboTimeout;
        UpdateComboText();
    }

    private void UpdateComboText()
    {
        comboText.text = combo > 0 ? "Combo x" + combo : "";
    }
}
