using System.Collections;
using UnityEngine;
using YG;

public class AdScript : MonoBehaviour
{
    public Hero1 heroScript1;
    public Hero heroScript;

    public int whatLevel = 1;

    private void OnEnable() => YG2.onRewardAdv += GetReward;
    private void OnDisable() => YG2.onRewardAdv -= GetReward;

    public void Awake()
    {
        Time.timeScale = 1.0f;
    }

    public void OpenAd()
    {
        YG2.RewardedAdvShow("gold_reward");
    }

    void GetReward(string id)
    {
        if (id == "gold_reward")
        {
            if (whatLevel == 3)
            {
                // 1. Возвращаем данные игрока
                heroScript1.currentLives = 1;
                heroScript1.isDead = false;
                heroScript1.UpdateLivesUI();

                // 2. ТЕПЕРЬ ВКЛЮЧАЕМ ПАНЕЛЬ ОБРАТНО (раскомментировано)
                if (heroScript1.losePanel != null)
                {
                    heroScript1.losePanel.SetActive(false);
                }

                // 3. Запускаем корутину с подготовкой
                StartCoroutine(ResumeWithDelay());
            }
            if (whatLevel == 1)
            {
                // 1. Возвращаем данные игрока
                heroScript.currentLives = 1;
                heroScript.isDead = false;
                heroScript.UpdateLivesUI();

                // 2. ТЕПЕРЬ ВКЛЮЧАЕМ ПАНЕЛЬ ОБРАТНО (раскомментировано)
                if (heroScript.losePanel != null)
                {
                    heroScript.losePanel.SetActive(false);
                }

                // 3. Запускаем корутину с подготовкой
                StartCoroutine(ResumeWithDelay());
            }
            Debug.Log("Награда получена!");
        }
    }


    IEnumerator ResumeWithDelay()
    {
        // Ждем один кадр для синхронизации с плагином
        yield return new WaitForEndOfFrame();

        // --- РЕШЕНИЕ МИНУСА №2 (Подготовка) ---
        // Оставляем время на паузе еще на 2 секунды, 
        // но используем WaitForSecondsRealtime, так как Time.timeScale = 0
        Debug.Log("Приготовьтесь...");

        // Тут можно вывести на экран текст "3... 2... 1..." если есть желание
        yield return new WaitForSecondsRealtime(2f);

        // Включаем время
        Time.timeScale = 1f;

        if (whatLevel == 3) { heroScript1.OnPointerUp(); }
        else if (whatLevel == 1) { heroScript.OnPointerUp(); }

        Debug.Log("Игра погнала! Время: " + Time.timeScale);
    }
}