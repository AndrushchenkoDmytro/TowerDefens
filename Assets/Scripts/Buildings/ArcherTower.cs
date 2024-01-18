using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Transform tempTarget;
    private float time = 0.7f;
    private float findTargeTimetInterval = 0.5f;
    private float distanceToTarget;
    private float atackRadius = 16;
    [SerializeField] private LayerMask enemyLayer;

    private bool isEnemyNearby = false;

    private void Update()
    {
        if (isEnemyNearby)
        {
            if(target == null)
            {
                time = 0;
                FindTarget();
            }
            RefreshTarget();
        }
    }
    private void RefreshTarget()
    {
        if (time < findTargeTimetInterval)
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
