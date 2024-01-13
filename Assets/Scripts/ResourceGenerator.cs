using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private ResourceTypes resourceType;
    [SerializeField] private float detectRadius = 8f;
    private int resourcesPerTimeInterval = 1;
    [SerializeField] private float generationTime = 0;
    [SerializeField] private float minGerenationTime = 0.4f;
    [SerializeField] private float maxGerenationTime = 0.8f;
    private float currentTime = 0;
    [SerializeField] private float resourceNodesAmount = 0;
    [SerializeField] private float requiredAmountNodes = 15;
    [SerializeField] LayerMask resourcesLayer;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CheckResourceAmount();
        if(resourceNodesAmount < 1)
        {
            OffGenerator();

        }
        else
        {
            CalculateGenerationSpeed();
        }
    }

    private void Update()
    {
        if(currentTime < generationTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            ResourceManager.Instance.AddResource(resourceType, resourcesPerTimeInterval);
        }
    }

    public void CalculateGenerationSpeed()
    {
        float tmp = minGerenationTime + (minGerenationTime - maxGerenationTime) * (resourceNodesAmount / requiredAmountNodes - 1);

        generationTime = Mathf.Clamp(tmp, minGerenationTime, maxGerenationTime);
    }

    public float GetGenerationSpeed()
    {
        if (generationTime <= 0) return 0;
        return resourcesPerTimeInterval / generationTime;
    }

    private void OffGenerator()
    {
        animator.enabled = false;
        enabled = false;
    }

    private void CheckResourceAmount()
    {
        Collider2D[] resourceNodes = Physics2D.OverlapCircleAll(transform.position, detectRadius, resourcesLayer);
        foreach (Collider2D node in resourceNodes)
        {
            if (node.gameObject.tag == resourceType.ToString())
                resourceNodesAmount++;
        }
    }

    public int GetProductionPerformans(Vector3 checkPosition)
    {
        float resourceNodesAmount = 0;
        Collider2D[] resourceNodes = Physics2D.OverlapCircleAll(checkPosition, detectRadius, resourcesLayer);
        if (resourceNodes.Length == 0) return 0;
        foreach (Collider2D node in resourceNodes)
        {
            if (node.gameObject.tag == resourceType.ToString())
                resourceNodesAmount++;
        }
        resourceNodesAmount = Mathf.Clamp(resourceNodesAmount, 0, requiredAmountNodes);

        return Mathf.RoundToInt(resourceNodesAmount / requiredAmountNodes * 100);
    }

    public ResourceTypes GetResourceType()
    {
        return resourceType;
    }
}
