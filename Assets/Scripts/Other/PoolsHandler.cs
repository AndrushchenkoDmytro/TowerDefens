using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsHandler : MonoBehaviour
{
    [SerializeField] private GameObject buildingsDestroyerPrefab;
    [SerializeField] private GameObject resourcesDestroyerPrefab;
    [SerializeField] private Transform enemysContainer;

    [SerializeField] private GameObject buildingConstructionPrefab;
    [SerializeField] private Transform buildingConstructionContainer;

    [SerializeField] private GameObject enemyParticlesPrefab;
    [SerializeField] private Transform enemyParticlesContainer;
    
    public static PoolsHandler instance;
    public ObjectPool<BuildingsDestroyerEnemy> buildingDestroyers { get; private set; }
    public ObjectPool<ResourcesDestroyerEnemy> resourceDestroyers { get; private set; }
    public ObjectPool<BuildingConstruction> buildingConstructions { get; private set; }
    public ObjectPool<EnemyParticals> enemyParticles { get; private set; }

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
        buildingDestroyers = new ObjectPool<BuildingsDestroyerEnemy>(buildingsDestroyerPrefab, enemysContainer);
        resourceDestroyers = new ObjectPool<ResourcesDestroyerEnemy>(resourcesDestroyerPrefab, enemysContainer);
        buildingConstructions = new ObjectPool<BuildingConstruction>(buildingConstructionPrefab, buildingConstructionContainer);
        enemyParticles = new ObjectPool<EnemyParticals>(enemyParticlesPrefab, enemyParticlesContainer);
    }
}
