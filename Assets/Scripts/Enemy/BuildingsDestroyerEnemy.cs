using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsDestroyerEnemy : MonoBehaviour, IEnemy, IPoolable
{
    private Transform mainTower;
    private Transform tempTarget;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 6;
    private float searchRadius = 12f;
    [SerializeField] private int buildingLayer;
    [SerializeField] private int projectileLayer;

    private float distanceToTarget = 0;
    private float time = 0.7f;
    private float findTargeTimetInterval = 0.8f;

    private Rigidbody2D rb;

    public event System.Action<IPoolable> OnDestroy;

    void Awake()
    {
        moveSpeed = Random.Range(5.5f, 6.5f);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainTower = PLayerController.Instance.MainTower();
        target = mainTower;
        CalculateMoveDirection();
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            MoveToTarget();
        }
        else
        {
            time = 0;
            FindTarget();
        }
        RefreshTarget();
    }

    private void MoveToTarget()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void RefreshTarget()
    {
        if(time < findTargeTimetInterval)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            FindTarget();
        }
    }

    private void FindTarget()
    {        
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, searchRadius, buildingLayer);
        if(target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);
        }
        else
        {
            distanceToTarget = float.MaxValue;
        }
        float tempDistance = 0;
        tempTarget = null;
        foreach (Collider2D target in targets)
        {
            tempDistance = Vector3.Distance(transform.position,target.transform.position);
            if(tempDistance <= distanceToTarget)
            {
                tempTarget = target.transform;
                distanceToTarget = tempDistance;
            }
        }
        if (tempTarget != null)
        {
            target = tempTarget;
        }
        else
        {
            target = mainTower;
        }
        if (target == null)
        {
            rb.velocity = Vector2.zero;
            enabled = false;
        }
        CalculateMoveDirection();
    }
    private void CalculateMoveDirection()
    {
        moveDirection = (target.position - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == buildingLayer)
        {
            collision.gameObject.GetComponent<HealthSystem>().GetDamage(10);
            OnDestroy?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == projectileLayer)
        {
            OnDestroy?.Invoke(this);
        }
    }

    public void Reset()
    {
        moveDirection = Vector3.zero;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
