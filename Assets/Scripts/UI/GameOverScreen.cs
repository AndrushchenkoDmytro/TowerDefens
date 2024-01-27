using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    private Animator animator;
    [SerializeField] private TextMeshProUGUI survivedCount;
    [SerializeField] private EnemyWavesManager enemyWavesManager;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameObject.Find("MainTower").GetComponent<MainTower>().OnGameOver += () => 
        {
            HideGameCanvas();
            ShowGameOverScreen();
        };
        transform.GetChild(3).GetComponent<Button>().onClick.AddListener( () =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); // retry
        });
        transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0); // mainMenu;
        });
        gameObject.SetActive(false);
    }

    private void HideGameCanvas()
    {
        gameUI.SetActive(false);
    }

    private void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        survivedCount.text = $"You Survived {enemyWavesManager.GetWaveIndex()} Waves";
        animator.Play("ShowGameOverScreen");
    }
}
