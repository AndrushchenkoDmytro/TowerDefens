using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ArrowProjectile : MonoBehaviour
{
    private Transform sprite;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 moveDirection;
    private float moveSpeed = 9;
    private float time = 0;
    [SerializeField] private float lifeTime = 2;
    private float rotateTime = 0.1f;
    private float rotateInterval = 0.1f;
    [SerializeField] private LayerMask projectileLayer;

    private bool isTargetActive = true;
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
            sprite.eulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVector(moveDirection));
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == projectileLayer)
        {
            Destroy(gameObject);
        }
    }
}
