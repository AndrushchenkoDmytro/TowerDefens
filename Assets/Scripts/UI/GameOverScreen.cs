using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
            Time.timeScale = 0;
            SoundManager.instance.PlaySound(SoundManager.Sound.GameOver);
            HideGameCanvas();
            ShowGameOverScreen();
        };
        transform.GetChild(3).GetComponent<Button>().onClick.AddListener( () =>
        {
            SceneLoadManager.instance.RetryLevel(); // retry
        });
        transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() =>
        {
        SceneLoadManager.instance.LoadLevel(0); // mainMenu;
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
