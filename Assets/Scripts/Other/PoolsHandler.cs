using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsHandler : MonoBehaviour
{
    [SerializeField] private GameObject buildingsDestroyerPrefab;
    [SerializeField] private Transform buildingsDestroyerContainer;

    [SerializeField] private GameObject resourcesDestroyerPrefab;
    [SerializeField] private Transform resourcesDestroyerContainer;

    public static PoolsHandler instance;
    public ObjectPool<BuildingsDestroyerEnemy> buildingDestroyers { get; private set; }
    public ObjectPool<ResourcesDestroyerEnemy> resourceDestroyers { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        buildingDestroyers = new ObjectPool<BuildingsDestroyerEnemy>(buildingsDestroyerPrefab, buildingsDestroyerContainer);
        resourceDestroyers = new ObjectPool<ResourcesDestroyerEnemy>(resourcesDestroyerPrefab, resourcesDestroyerContainer);
    }
}
