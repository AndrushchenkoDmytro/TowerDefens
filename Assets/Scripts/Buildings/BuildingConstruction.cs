using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingConstruction : MonoBehaviour, IPoolable
{
    private float elapsedTime = 0;
    private float constructionTime = 2;
    private SpriteRenderer spriteRenderer;
    private Transform constructionPrefab;
    private BoxCollider2D boxCollider;
    private BuildingTypeHolder buildingTypeHolder;
    private HealthSystem healthSystem;
    [SerializeField] private Image progressBar;
    [SerializeField] private Material constructionMaterial;


    public event System.Action<IPoolable> OnPolableDestroy;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        constructionMaterial = spriteRenderer.material;
        boxCollider = GetComponent<BoxCollider2D>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / constructionTime;
        constructionMaterial.SetFloat("_Progress", progress);
        progressBar.fillAmount = progress;
        if(elapsedTime > constructionTime)
        {
            Instantiate(constructionPrefab, transform.position, Quaternion.identity);
            OnPolableDestroy?.Invoke(this);
        }
    }

    public void SetBuildingType(BuildingsTypeSo buildingsTypeSo)
    {
        buildingTypeHolder.SetHolderBuildingType(buildingsTypeSo);
        gameObject.tag = buildingsTypeSo.prefab.tag;
        constructionPrefab = buildingsTypeSo.prefab;
        constructionTime = buildingsTypeSo.constractionTime;
        spriteRenderer.sprite = buildingsTypeSo.buildingSprite;
        BoxCollider2D box = buildingsTypeSo.prefab.gameObject.GetComponent<BoxCollider2D>();
        boxCollider.size = box.size;
        boxCollider.offset = box.offset;
    }

    public void Reset()
    {
        healthSystem.ResetToDefault();
        elapsedTime = 0;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        OnPolableDestroy = null;
    }
}
