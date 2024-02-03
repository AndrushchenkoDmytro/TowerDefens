using UnityEngine;

public class MainTower : MonoBehaviour
{
    private HealthSystem healthSystem;
    public event System.Action OnGameOver;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDiedEvent += GameOver;
    }

    private void GameOver(System.Object sender, System.EventArgs e)
    {
        OnGameOver?.Invoke();
    }
}
