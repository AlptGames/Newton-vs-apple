using UnityEngine;
using UnityEngine.UI; // Обязательно для работы с UI

public class CircularTimer : MonoBehaviour
{
    public Image timerImage; // Сюда перетащите вашу картинку
    public float maxTime = 10.0f; // Время таймера в секундах
    private float currentTime;

    public GameObject ADButton;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Уменьшаем время каждый кадр
            // Рассчитываем процент заполнения (от 1 до 0)
            timerImage.fillAmount = currentTime / maxTime;
        }
        else
        {
            ADButton.SetActive(false);
        }
    }
}