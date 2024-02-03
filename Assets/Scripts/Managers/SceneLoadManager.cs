using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    public int currentLevelIndex { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevel(int levelIndex)
    {      
        currentLevelIndex = levelIndex;
        SceneManager.LoadSceneAsync(levelIndex);
        Time.timeScale = 1;
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(currentLevelIndex);
        Time.timeScale = 1;
    }
}
