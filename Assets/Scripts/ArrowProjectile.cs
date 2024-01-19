using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ArrowProjectile : MonoBehaviour
{
    private Transform sprite;
    private Transform target;
    private Vector3 moveDirection;
    private float moveSpeed = 8;
    private float time = 0;
    [SerializeField] private float lifeTime = 2;
    private float rotateTime = 0f;
    private float rotateInterval = 0.1f;
    [SerializeField] private LayerMask projectileLayer;
    private void Start()
    {
        sprite = transform.GetChild(0).transform;
    }

    private void Update()
    {
        MoveToTarget();
        RefreshRotation();
        CheckLifeTime();
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
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
            sprite.eulerAngles = new Vector3(0,0,GetAngleFromVector(moveDirection));
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

    private float GetAngleFromVector(Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == projectileLayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
