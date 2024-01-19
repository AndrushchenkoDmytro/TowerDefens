using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    private Transform tempTarget;
    private float rechargeTime = 0.4f;
    [SerializeField][Range(0.3f,0.8f)] private float rechargeInterval = 0.4f;
    private float timeToFindTarget = 0.5f;
    private float findTargeTimetInterval = 0.5f;
    private float distanceToTarget;
    [SerializeField] private float atackRadius = 16;
    [SerializeField] private LayerMask enemyLayer;

    private bool isEnemyNearby = false;

    private void Update()
    {
        if (isEnemyNearby)
        {
            if(target == null)
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
            GameObject projectile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
            projectile.GetComponent<ArrowProjectile>().SetTarget(target);
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
        if (target != null)
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
                isEnemyNearby = true;
            }
        }
    }
}
