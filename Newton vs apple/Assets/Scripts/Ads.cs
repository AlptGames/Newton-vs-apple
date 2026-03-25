using UnityEngine;
using YG; // Оставляем этот namespace

public class Ads : MonoBehaviour
{
    public Hero script;

    private void Start()
    {
        script = GetComponent<Hero>();
    }

    private void OnEnable()
    {
        // Подписываемся на событие (метод должен принимать string)
        YG2.onRewardAdv += Rewarded;
    }

    private void OnDisable()
    {
        // Обязательно отписываемся, чтобы избежать утечек памяти
        YG2.onRewardAdv -= Rewarded;
    }

    // Обработчик события
    void Rewarded(string id)
    {
        // Если у вас несколько типов наград, можно проверять id
        script.money += 500;
        Debug.Log("Игрок получил награду!");
    }

    // Метод для вызова самой рекламы (привяжите к кнопке)
    public void ShowAd()
    {
        // Вызов окна с рекламой за вознаграждение
        YG2.RewardedAdvShow("награда_1");
    }
}