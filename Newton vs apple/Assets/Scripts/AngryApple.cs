using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryApple : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Массив префабов предметов (например, яблок)
    public float initialSpawnRate = 2f; // Начальный интервал между спавном предметов (в секундах)
    public float spawnRateDecrease = 0.1f; // Величина, на которую уменьшается интервал спавна
    public float spawnRateDecreaseInterval = 10f; // Как часто уменьшается интервал спавна (каждые N секунд)
    public float spawnAreaLeft = -5f; // Левая граница зоны спавна по оси X
    public float spawnAreaRight = 5f; // Правая граница зоны спавна по оси X
    public float spawnYPosition = 10f; // Высота, с которой падают предметы
    public string floorTag = "Floor"; // Тэг объектов, считающихся полом
      public string PlayerTag = "Player"; // Тэг пола


    private float currentSpawnRate;
    private float spawnTimer;
    private float rateDecreaseTimer;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        spawnTimer = initialSpawnRate; // Запускаем таймер спавна сразу
        rateDecreaseTimer = spawnRateDecreaseInterval;
    }

    void Update()
    {
        HandleTimers();
        SpawnItems();
    }

    void HandleTimers()
    {
        spawnTimer -= Time.deltaTime;
        rateDecreaseTimer -= Time.deltaTime;

        // Уменьшаем интервал спавна со временем, делая спавн чаще
        if (rateDecreaseTimer <= 0)
        {
            currentSpawnRate -= spawnRateDecrease;
            // Ограничиваем минимальный интервал, чтобы спавн не стал слишком частым
            currentSpawnRate = Mathf.Max(currentSpawnRate, 0.5f); // Минимум 0.5 секунды
            rateDecreaseTimer = spawnRateDecreaseInterval; // Сбрасываем таймер уменьшения скорости
        }
    }

    void SpawnItems()
    {
        // Если таймер спавна истек и есть предметы для спавна
        if (spawnTimer <= 0 && itemPrefabs != null && itemPrefabs.Length > 0)
        {
            // Выбираем случайный префаб из массива
            GameObject prefabToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            // Определяем случайную позицию для спавна по оси X в заданных границах
            float randomX = Random.Range(spawnAreaLeft, spawnAreaRight);
            Vector3 spawnPosition = new Vector3(randomX, spawnYPosition, 0f);

            // Создаем экземпляр предмета
            GameObject spawnedItem = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Добавляем компонент ItemDestruction, чтобы предмет мог исчезнуть при касании пола
            if (spawnedItem.GetComponent<ItemDestruction>() == null)
            {
                spawnedItem.AddComponent<ItemDestruction>().floorTag = floorTag;
            }

            // Сбрасываем таймер спавна
            spawnTimer = currentSpawnRate;
        }
    }
}

// Отдельный скрипт для предметов, падающих с неба
public class ItemDestruction : MonoBehaviour
{
    public string floorTag; // Тэг пола

     public string PlayerTag; // Тэг пола

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулся ли предмет с полом
        if (collision.gameObject.CompareTag(floorTag))
        {
            Destroy(gameObject); // Уничтожаем предмет
        }
    }

}
