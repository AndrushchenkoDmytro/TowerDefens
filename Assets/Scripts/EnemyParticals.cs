using System;
using UnityEngine;

public class EnemyParticals : MonoBehaviour, IPoolable
{
    private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    public event Action<IPoolable> OnPolableDestroy;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void SetStartColor(Color color)
    {
        mainModule.startColor = color;
    }

    public void PlayParticles()
    {
        particleSystem.Play();
        Invoke("ReturnToPool", 2f);
    }

    private void ReturnToPool()
    {
        OnPolableDestroy?.Invoke(this);
    }
}
