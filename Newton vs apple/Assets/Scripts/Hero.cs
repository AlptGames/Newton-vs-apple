using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Hero : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость передвижения игрока
    public int maxLives = 3;     // Максимальное количество жизней
    private int currentLives;    // Текущее количество жизней

    public GameObject[] lifeHearts; // Массив UI Image для отображения сердец
    public string hazardTag = "Hazard"; // Тэг для объектов, наносящих урон
    public string gameOverSceneName = "GameOver"; // Название сцены для проигрыша
    public GameObject losePanel; // Панель, отображающая сообщение о проигрыше

    private Rigidbody2D rb;       // Компонент Rigidbody2D для физики
    private bool facingRight = true; // Направление взгляда игрока
    private bool isDead = false; // Флаг, указывающий, мертв ли игрок

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
        UpdateLivesUI(); // Инициализация UI жизней

        // Убедимся, что панель проигрыша изначально скрыта
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Если игрок мертв, прекращаем обработку ввода
        if (isDead)
        {
            return;
        }

        // Получаем ввод по горизонтальной оси
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Двигаем игрока
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Разворот спрайта игрока
        HandleFlip(horizontalInput);
    }

    void HandleFlip(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // Инвертируем масштаб по оси X
        transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Проверяем столкновение с объектом, имеющим указанный тэг
        if (other.gameObject.CompareTag(hazardTag))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        // Если игрок уже мертв, не отнимаем жизни
        if (isDead)
        {
            return;
        }

        currentLives--; // Отнимаем жизнь
        UpdateLivesUI(); // Обновляем отображение жизней

        if (currentLives <= 0)
        {
            isDead = true; // Устанавливаем флаг смерти
            StartCoroutine(HandleGameOver()); // Запускаем корутину для завершения игры
        }
    }

    void UpdateLivesUI()
    {
        // Отключаем изображения сердец, начиная с конца массива
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            if (i < currentLives)
            {
                lifeHearts[i].SetActive(true); // Показываем сердце
            }
            else
            {
                lifeHearts[i].SetActive(false); // Скрываем сердце
            }
        }
    }

    IEnumerator HandleGameOver()
    {
        // Показываем панель проигрыша
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        // Замораживаем время, чтобы показать панель
        Time.timeScale = 0f;

        // Ждем 0.5 секунды
        yield return new WaitForSecondsRealtime(0.5f);

        // Возвращаем время к норме (если нужно для других процессов)
        Time.timeScale = 1f;

        // Загружаем сцену проигрыша
        SceneManager.LoadScene(gameOverSceneName);
    }
}
