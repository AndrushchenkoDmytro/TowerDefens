using UnityEngine;

public class CanonTower : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    private Transform tempTarget;
    private bool spawn = false;
    private float timeToFindTarget = 0.4f;
    private float findTargeTimetInterval = 0.4f;
    private float distanceToTarget;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float atackRadius = 16;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;

    private bool isEnemyNearby = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isEnemyNearby)
        {
            if (target != null && target.gameObject.activeInHierarchy == false)
            {
                timeToFindTarget = 0;
                FindTarget();
            }
            RefreshTarget();
            Shoot();
        }
    }

    private void Shoot()
    {
        if(spawn && target != null)
        {
            CanonProjectile projectile = PoolsHandler.instance.canonProjectile.GetObjectFromPool(spawnPos.position);
            projectile.SetTarget(target);
            spawn = false;
        }
    }

    private void RefreshTarget()
    {
        if (timeToFindTarget < findTargeTimetInterval)
        {
            timeToFindTarget += Time.deltaTime;
        }
        else
        {
            timeToFindTarget = 0;
            FindTarget();
        }
    }
    private void FindTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, atackRadius, enemyLayer);
        if (target != null && target.gameObject.activeInHierarchy)
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
            tempDistance = Vector3.Distance(transform.position, target.transform.position);
            if (tempDistance <= distanceToTarget)
            {
                tempTarget = target.transform;
                distanceToTarget = tempDistance;
            }
        }

        target = tempTarget;
        if (target == null)
        {
            isEnemyNearby = false;
        }
        else
        {
            animator.Play("CanonFire");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isEnemyNearby == false)
            {
                timeToFindTarget = findTargeTimetInterval;
                isEnemyNearby = true;
            }
        }
    }

    public void Recharge()
    {
        spawn = true;
    }
}
