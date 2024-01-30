using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PLayerController : MonoBehaviour
{
    [SerializeField] private Transform mainTower;
    public static PLayerController instance;

    [SerializeField] private Transform ghost;        
    private Camera mainCamera;
    private Vector2 screenCenter;
    private bool isPointerOverGameobject = false;
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
        if(instance == null)
        {
            instance = this;
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
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);


    }
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (isPointerOverGameobject == false)
                {
                    Vector3 touchWorldPos =  mainCamera.ScreenToWorldPoint(touch.position);
                    touchWorldPos.z = 0;
                    Vector3 moveDir = (ghost.position - touchWorldPos).normalized;
                    moveDir.x *= 1.4f;
                    ghost.position -= moveDir * 15 * Time.deltaTime;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if(isPointerOverGameobject == false)
                    {
                        SpawnBuilding();
                    }
                    isPointerOverGameobject = false;
                }
            }
            else
            {
                isPointerOverGameobject = true;
            }
        }
    }

    private void SpawnBuilding()
    {
        if(activeBuildingType != null && CanSpawnBuilding() == true)
        {
            if (ResourceManager.Instance.CanAfford(activeBuildingType.constructPriceList))
            {
                buildingConstruction = PoolsHandler.instance.buildingConstructions.GetObjectFromPool(ghost.position);
                buildingConstruction.SetBuildingType(activeBuildingType);
                SoundManager.instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            }
        }
    }

    public void SetActiveBuildingType(BuildingsTypeSo buildingsTypeSo)
    {
        activeBuildingType = buildingsTypeSo;
        Vector3 center = mainCamera.ScreenToWorldPoint(screenCenter);
        center.z = 0;
        ghost.position = center;
        OnActiveBuildingChanged?.Invoke(this,new OnActiveBuildingChangedArgs { activeBuilding = activeBuildingType});
    }

    private bool CanSpawnBuilding()
    {
        BoxCollider2D activeBuildingCollider2D = activeBuildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D boxOverlap2D = Physics2D.OverlapBox(ghost.position + (Vector3)activeBuildingCollider2D.offset, activeBuildingCollider2D.size, 0,layerBitMask);
        if (boxOverlap2D != null)
        {
            ToolTips.Instance.ShowNotEnoughSpaceTip(); 
            return false;
        };

        Collider2D[] boxOverlapArray = Physics2D.OverlapCircleAll(ghost.position, activeBuildingType.blockConstracionRadius, buildingLayer);
        
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
