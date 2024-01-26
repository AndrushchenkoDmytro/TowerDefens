using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsDestroyerEnemy : MonoBehaviour, IEnemy, IPoolable
{
    private Transform mainTower;
    private Transform tempTarget;
    [SerializeField] private Transform target;
    private Vector3 targetLastPos;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 6;
    private float searchRadius = 12f;
    [SerializeField] private LayerMask buildingLayerMask;
    [SerializeField] private int buildingLayer;
    [SerializeField] private int projectileLayer;

    private float distanceToTarget = 0;
    [SerializeField] private float time = 0.5f;
    private float refreshTargeInterval = 0.65f;

    private Rigidbody2D rb;

    public event System.Action<IPoolable> OnPolableDestroy;

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
        if(time < refreshTargeInterval)
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
        Debug.Log("buildingLayer = "+buildingLayer);
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, searchRadius, buildingLayerMask);
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
        foreach (Collider2D targetCollider in targets)
        {
            Debug.Log(targetCollider.transform.gameObject.name + " = " + targetCollider.transform.gameObject.layer);
            tempDistance = Vector3.Distance(transform.position,targetCollider.transform.position);
            if(tempDistance <= distanceToTarget)
            {
                tempTarget = targetCollider.transform;
                distanceToTarget = tempDistance;
            }
        }
        if (tempTarget != null)
        {
            target = tempTarget;
            targetLastPos = target.position;
        }
        else
        {
            target = mainTower;
            if(target != null) { targetLastPos = target.position; }
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
        moveDirection = (targetLastPos - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == buildingLayer)
        {
            collision.gameObject.GetComponent<HealthSystem>().GetDamage(10);
            OnPolableDestroy?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == projectileLayer)
        {
            OnPolableDestroy?.Invoke(this);
        }
    }

    public void Reset()
    {
        moveDirection = Vector3.zero;
        rb.velocity = Vector2.zero;
        time = refreshTargeInterval;
        target = null;
        gameObject.SetActive(false);
    }
}
