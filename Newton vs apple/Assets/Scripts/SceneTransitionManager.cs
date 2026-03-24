
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public string sceneName; // Имя сцены, на которую нужно перейти

    // Метод, который будет вызываться при нажатии на кнопку
    public void LoadNextScene()
    {
        // Проверяем, что имя сцены установлено
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Загружаем указанную сцену
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Имя сцены для перехода не установлено!");
        }
    }
}
