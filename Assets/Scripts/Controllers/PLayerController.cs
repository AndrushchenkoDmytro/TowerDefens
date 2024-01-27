using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PLayerController : MonoBehaviour
{
    [SerializeField] private Transform mainTower;
    public static PLayerController Instance;
    public Vector3 mousePosition { get; private set; }
    private Camera mainCamera;
    public BuildingsTypeSo activeBuildingType { get; private set; }
    [SerializeField] private LayerMask buildingLayer;
    private int layerBitMask = 1 << 6 | 1 << 7;
    public EventHandler<OnActiveBuildingChangedArgs> OnActiveBuildingChanged;

    BuildingConstruction buildingConstruction;
    public Transform MainTower()
    {
        return mainTower;
    }
    public class OnActiveBuildingChangedArgs : EventArgs
    {
        public BuildingsTypeSo activeBuilding;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mainTower.GetComponent<MainTower>().OnGameOver += () => 
        {
            gameObject.SetActive(false);
            enabled = false;
        };
    }
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            UpdateMousePosition();
            SpawnBuilding();
        }
    }

    private void UpdateMousePosition()
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        mousePosition = worldPos;
    }

    private void SpawnBuilding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(activeBuildingType != null && CanSpawnBuilding() == true)
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructPriceList))
                {
                    buildingConstruction = PoolsHandler.instance.buildingConstructions.GetObjectFromPool(mousePosition);
                    buildingConstruction.SetBuildingType(activeBuildingType);
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingsTypeSo buildingsTypeSo)
    {
        activeBuildingType = buildingsTypeSo;
        OnActiveBuildingChanged?.Invoke(this,new OnActiveBuildingChangedArgs { activeBuilding = activeBuildingType});
    }

    private bool CanSpawnBuilding()
    {
        BoxCollider2D activeBuildingCollider2D = activeBuildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D boxOverlap2D = Physics2D.OverlapBox(mousePosition + (Vector3)activeBuildingCollider2D.offset, activeBuildingCollider2D.size, 0,layerBitMask);
        if (boxOverlap2D != null)
        {
            ToolTips.Instance.ShowNotEnoughSpaceTip(); 
            return false;
        };

        Collider2D[] boxOverlapArray = Physics2D.OverlapCircleAll(mousePosition, activeBuildingType.blockConstracionRadius, buildingLayer);
        
        foreach (Collider2D building in boxOverlapArray)
        {
            if(building.GetComponent<BuildingTypeHolder>().GetHolderBuildingType().name == activeBuildingType.name)
            {
                ToolTips.Instance.ShowVeryCloseToBuildingOfSameTypeTip();
                return false;
            }
        }
        return true;
    }

}
