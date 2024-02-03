using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    private Transform tempTarget;
    private float rechargeTime = 0.4f;
    [SerializeField][Range(0.3f,0.8f)] private float rechargeInterval = 0.4f;
    private float timeToFindTarget = 0.4f;
    private float findTargeTimetInterval = 0.4f;
    private float distanceToTarget;
    [SerializeField] private float atackRadius = 16;
    [SerializeField] private LayerMask enemyLayer;

    private bool isEnemyNearby = false;

    private void Start()
    {
        rechargeTime = rechargeInterval;
    }

    private void Update()
    {
        if (isEnemyNearby)
        {
            if(target != null && target.gameObject.activeInHierarchy == false)
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
        if (rechargeTime < rechargeInterval)
        {
            rechargeTime += Time.deltaTime;
        }
        else
        {
            rechargeTime = 0;
            ArrowProjectile projectile = PoolsHandler.instance.arrowProjectile.GetObjectFromPool(transform.position);
            projectile.SetTarget(target);
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
        if(target == null)
        {
            isEnemyNearby = false;
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
}
