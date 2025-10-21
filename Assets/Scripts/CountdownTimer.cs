using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 180f; // 3 минуты в секундах
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
            // Здесь можно вызвать функцию проигрыша, если время вышло
        }
    }
}
