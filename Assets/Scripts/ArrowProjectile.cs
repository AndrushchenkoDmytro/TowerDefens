using System;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour, IPoolable
{
    private Transform sprite;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 moveDirection;
    private float moveSpeed = 9;
    private float time = 0;
    [SerializeField] private float lifeTime = 2;
    private float rotateTime = 0.1f;
    private float rotateInterval = 0.1f;
    [SerializeField] private LayerMask enemyLayer;

    private bool isTargetActive = true;

    public event Action<IPoolable> OnPolableDestroy;

    private void Start()
    {
        sprite = transform.GetChild(0).transform;
    }

    private void Update()
    {
        MoveToTarget();
        CheckLifeTime();
    }

    private void MoveToTarget()
    {
        if (target != null && isTargetActive)
        {
            if(target.gameObject.activeInHierarchy == false)
            {
                isTargetActive = false;
            }
            else
            {
                moveDirection = (target.position - transform.position).normalized;
                RefreshRotation();
            }
        }
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void RefreshRotation()
    {
        if(rotateTime < rotateInterval)
        {
            rotateTime += Time.deltaTime;
        }
        else
        {
            
            rotateTime = 0;
            if (isTargetActive)
            {
                sprite.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));
            }
        }
    }

    private void CheckLifeTime()
    {
        if(time< lifeTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            OnPolableDestroy?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            OnPolableDestroy?.Invoke(this);
        }
    }

    public void Reset()
    {
        target = null;
        rotateTime = rotateInterval;
        time = 0;
        moveDirection = Vector3.zero;
        isTargetActive = true;
        gameObject.SetActive(false);
    }
}
