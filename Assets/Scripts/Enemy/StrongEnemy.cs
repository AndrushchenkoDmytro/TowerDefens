using System;
using UnityEngine;

public class StrongEnemy : MonoBehaviour, IPoolable, IEnemy
{
    private Transform target;
    private Vector3 moveDirection;
    private float moveSpeed = 12f;
    [SerializeField] private int buildingLayer;
    [SerializeField] private int projectileLayer;
    private Rigidbody2D rb;
    [SerializeField] private Color particleColor;

    public event Action<IPoolable> OnPolableDestroy;

    private void Start()
    {
        target = PLayerController.instance.MainTower();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == buildingLayer)
        {
            collision.gameObject.GetComponent<HealthSystem>().GetDamage(30);
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == projectileLayer)
        {
            ReturnToPool();
        }
    }
    private void ReturnToPool()
    {
        EnemyParticals ep = PoolsHandler.instance.enemyParticles.GetObjectFromPool(transform.position);
        ep.SetStartColor(particleColor);
        ep.PlayParticles();
        SoundManager.instance.PlaySound(SoundManager.Sound.EnemyDie);
        OnPolableDestroy?.Invoke(this);
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        ReturnToPool();
    }
}
