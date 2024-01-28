using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    private bool needLoadScreen = true;
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
        /*if (needLoadScreen)
        {
           show load screen level;
        }
        else
        {
           load target scene;
        }
         */
        
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
