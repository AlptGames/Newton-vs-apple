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
    [HideInInspector] public int currentLives;    // Текущее количество жизней

    public GameObject[] lifeHearts; // Массив UI Image для отображения сердец
    public string hazardTag = "Hazard"; // Тэг для объектов, наносящих урон
    public string gameOverSceneName = "GameOver"; // Название сцены для проигрыша
    public GameObject losePanel; // Панель, отображающая сообщение о проигрыше

    private Rigidbody2D rb;       // Компонент Rigidbody2D для физики
    private bool facingRight = true; // Направление взгляда игрока
    [HideInInspector] public bool isDead = false; // Флаг, указывающий, мертв ли игрок
    private float horizontalMove = 0f;
    public AudioSource damageSound;
    public AudioSource GameOVerSound;

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
        if (isDead) return;

        // Складываем ввод с клавиатуры и с экранных кнопок
        // Mathf.Clamp ограничит значение в диапазоне от -1 до 1
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float combinedInput = Mathf.Clamp(keyboardInput + horizontalMove, -1f, 1f);

        rb.velocity = new Vector2(combinedInput * moveSpeed, rb.velocity.y);

        HandleFlip(combinedInput);
    }

    public void OnPointerDownLeft() => horizontalMove = -1f;
    public void OnPointerDownRight() => horizontalMove = 1f;
    public void OnPointerUp() => horizontalMove = 0f;

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
            Destroy(other.gameObject);
        }
    }

   public void TakeDamage()
    {
        // Если игрок уже мертв, не отнимаем жизни
        if (isDead)
        {
            return;
        }

        currentLives--; // Отнимаем жизнь
        damageSound.Play();
        UpdateLivesUI(); // Обновляем отображение жизней

        if (currentLives <= 0)
        {
            isDead = true; // Устанавливаем флаг смерти
            if (losePanel != null)
            {
                losePanel.SetActive(true);
                GameOVerSound.Play();
                Time.timeScale = 0f;
            }
        }
    }

    public void UpdateLivesUI()
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
}
