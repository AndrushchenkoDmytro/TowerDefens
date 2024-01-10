using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private Resource resourceType;
    [SerializeField] private int maxResourcesPerTimeInterval = 4;
    [SerializeField] private int resourcesPerTimeInterval;
    [SerializeField] private float timeInterval = 2;
    [SerializeField] private float detectRadius = 8f;
    private float currentTime = 0;
    [SerializeField] private float resourceNodesAmount = 0;
    private float requiredAmountNodes = 15;
    [SerializeField] LayerMask resourcesLayer;

    private void Start()
    {
        CheckResourceAmount();
        if(resourceNodesAmount <= 1)
        {
            enabled = false;
        }
        else
        {
            float tmp = resourceNodesAmount / requiredAmountNodes * maxResourcesPerTimeInterval;
            
            resourcesPerTimeInterval = Mathf.Clamp(Mathf.RoundToInt(tmp), 0, maxResourcesPerTimeInterval);
        }
    }

    private void Update()
    {
        if(currentTime < timeInterval)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            ResourceManager.Instance.AddResource(resourceType, resourcesPerTimeInterval);
        }
    }

    public void CheckResourceAmount()
    {
        Collider2D[] resourceNodes =  Physics2D.OverlapCircleAll(transform.position, detectRadius, resourcesLayer);
        foreach(Collider2D node in resourceNodes)
        {
            if (node.gameObject.tag == resourceType.ToString())
                resourceNodesAmount++;
        }
    }
}
