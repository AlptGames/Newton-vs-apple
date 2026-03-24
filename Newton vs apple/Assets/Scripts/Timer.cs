using UnityEngine;
using TMPro; // Импортируем библиотеку TextMeshPro

public class Timer : MonoBehaviour
{
    public float timerStartTime = 0f; // Начальное значение таймера (обычно 0)
    private float currentTime;       // Текущее значение таймера
    private TextMeshProUGUI timerText; // Ссылка на компонент TextMeshProUGUI

    void Start()
    {
        // Находим компонент TextMeshProUGUI на объекте, к которому прикреплен скрипт.
        // Если у вас outro TextMeshPro компонент находится на другом объекте,
        // вам нужно будет соответствующим образом изменить эту строку (например, GetComponentInChildren, FindObjectOfType и т.д.).
        timerText = GetComponent<TextMeshProUGUI>();

        // Устанавливаем начальное значение таймера
        currentTime = timerStartTime;

        // Обновляем текст таймера при старте
        UpdateTimerDisplay();
    }

    void Update()
    {
        // Обновляем текущее время таймера, добавляя прошедшее время с последнего кадра
        currentTime += Time.deltaTime;

        // Обновляем отображение таймера на экране
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Форматируем время в строку (например, 00:00:00 или 00:00)
        // В данном примере используется формат минут:секунд. Миллисекунды не отображаются.
        int minutes = Mathf.FloorToInt(currentTime / 60f); // Вычисляем минуты
        int seconds = Mathf.FloorToInt(currentTime % 60f); // Вычисляем секунды

        // Обновляем текст компонента TextMeshProUGUI
        // Формат "00:00" гарантирует, что всегда будут две цифры для минут и секунд,
        // добавляя ведущий ноль при необходимости.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
