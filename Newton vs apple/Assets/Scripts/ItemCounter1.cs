using UnityEngine;
using TMPro; // Используем TextMeshPro для отображения текста

public class ItemCounter1 : MonoBehaviour
{
    public string hazardTag = "Hazard"; // Тэг объектов, которые будем считать
    public string floorTag = "Floor";   // Тэг объекта, с которым происходит столкновение
    public TextMeshProUGUI countText;   // Ссылка на компонент TextMeshProUGUI для отображения счета

    [HideInInspector] public int itemCount = 0; // Текущее количество предметов

    public Hero1 hero;

    void Start()
    {
        // Инициализируем отображение счетчика при старте
        UpdateCountDisplay();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулся ли объект с тэгом Hazard с объектом с тэгом Floor
        if (collision.gameObject.CompareTag(hazardTag))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Дополнительная проверка, чтобы быть уверенным, что столкновение произошло именно с полом
                if (contact.otherCollider.CompareTag(floorTag))
                {
                    hero.TakeDamage();
                }
            }
        }
    }


    public void UpdateCountDisplay()
    {
        // Обновляем текст в TextMeshProUGUI
        // Формат "000" гарантирует, что будет отображаться минимум 3 цифры с ведущими нулями
        if (countText != null)
        {
            countText.text = string.Format("{0:000}", itemCount);
        }
    }
}
