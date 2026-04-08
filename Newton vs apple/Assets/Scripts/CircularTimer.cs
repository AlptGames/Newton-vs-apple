using UnityEngine;
using UnityEngine.UI; // Обязательно для работы с UI

public class CircularTimer : MonoBehaviour
{
    public Image timerImage; // Сюда перетащите вашу картинку
    public float maxTime = 10.0f; // Время таймера в секундах
    private float currentTime;

    public GameObject ADButton;

    void OnEnable()
    {
        currentTime = maxTime;
        timerImage.fillAmount = 1f; // Сразу ставим полное заполнение
    }

    void Update()
    {
        if (currentTime > 0)
        {
            // Используем unscaledDeltaTime, чтобы таймер шел даже при Time.timeScale = 0
            currentTime -= Time.unscaledDeltaTime;

            timerImage.fillAmount = currentTime / maxTime;
        }
        else
        {
            ADButton.SetActive(false);
        }
    }
}