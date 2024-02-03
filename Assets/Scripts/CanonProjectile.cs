using System;
using UnityEngine;

public class CanonProjectile : MonoBehaviour, IPoolable
{
    private Transform sprite;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 moveDirection;
    private float inisialSpeed = 9;
    private float moveSpeed = 9;
    private float time = 0;
    [SerializeField] private float lifeTime = 3;
    private float rotateTime = 0.1f;
    private float rotateInterval = 0.1f;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;

    private bool isTargetActive = true;

    public event Action<IPoolable> OnPolableDestroy;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
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
            if (target.gameObject.activeInHierarchy == false)
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
        moveSpeed -= Time.deltaTime * 0.2f;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void RefreshRotation()
    {
        if (rotateTime < rotateInterval)
        {
            rotateTime += Time.deltaTime;
        }
        else
        {
            rotateTime = 0;
            sprite.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));
        }
    }

    private void CheckLifeTime()
    {
        if (time < lifeTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            Explosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        moveDirection = Vector3.zero;
        animator.Play("Explosion");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 4, enemyLayer);
        if (targets.Length > 0)
        {
            foreach (Collider2D t in targets)
            {
                t.GetComponent<IEnemy>().Kill();
            }
        }
    }

    public void OnExplosionEnd()
    {
        OnPolableDestroy?.Invoke(this);
    }
    public void Reset()
    {
        target = null;
        moveSpeed = inisialSpeed;
        rotateTime = rotateInterval;
        time = 0;
        moveDirection = Vector3.zero;
        isTargetActive = true;
        gameObject.SetActive(false);
    }
}
