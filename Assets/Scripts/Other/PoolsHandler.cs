using UnityEngine;

public class PoolsHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainTowerDestroyerPrefab;
    [SerializeField] private GameObject buildingsDestroyerPrefab;
    [SerializeField] private GameObject resourcesDestroyerPrefab;
    [SerializeField] private Transform enemysContainer;

    [SerializeField] private GameObject buildingConstructionPrefab;
    [SerializeField] private Transform buildingConstructionContainer;

    [SerializeField] private GameObject enemyParticlesPrefab;
    [SerializeField] private Transform enemyParticlesContainer;

    [SerializeField] private GameObject canonProjectilePrefab;
    [SerializeField] private GameObject arrowProjectilePrefab;
    [SerializeField] private Transform projectileContainer;


    public static PoolsHandler instance;
    public ObjectPool<StrongEnemy> mainTowerDestroyers { get; private set; }
    public ObjectPool<SimpleEnemy> buildingDestroyers { get; private set; }
    public ObjectPool<SimpleEnemy> resourceDestroyers { get; private set; }
    public ObjectPool<BuildingConstruction> buildingConstructions { get; private set; }
    public ObjectPool<EnemyParticals> enemyParticles { get; private set; }
    public ObjectPool<ArrowProjectile> arrowProjectile { get; private set; }
    public ObjectPool<CanonProjectile> canonProjectile { get; private set; }

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
        mainTowerDestroyers = new ObjectPool<StrongEnemy>(mainTowerDestroyerPrefab, enemysContainer);
        buildingDestroyers = new ObjectPool<SimpleEnemy>(buildingsDestroyerPrefab, enemysContainer);
        resourceDestroyers = new ObjectPool<SimpleEnemy>(resourcesDestroyerPrefab, enemysContainer);
        buildingConstructions = new ObjectPool<BuildingConstruction>(buildingConstructionPrefab, buildingConstructionContainer);
        enemyParticles = new ObjectPool<EnemyParticals>(enemyParticlesPrefab, enemyParticlesContainer);
        arrowProjectile = new ObjectPool<ArrowProjectile>(arrowProjectilePrefab, projectileContainer);
        canonProjectile = new ObjectPool<CanonProjectile>(canonProjectilePrefab, projectileContainer);
    }
}
