using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PLayerController : MonoBehaviour
{
    public static PLayerController Instance;

    public Vector3 mousePosition { get; private set; }
    private Camera mainCamera;
    public BuildingsTypeSo activeBuildingType { get; private set; }

    public EventHandler<OnActiveBuildingChangedArgs> OnActiveBuildingChanged;

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
            if(activeBuildingType != null)
            {
                Instantiate(activeBuildingType.prefab, mousePosition, Quaternion.identity);
            }
        }
    }

    public void SetActiveBuildingType(BuildingsTypeSo buildingsTypeSo)
    {
        activeBuildingType = buildingsTypeSo;
        OnActiveBuildingChanged?.Invoke(this,new OnActiveBuildingChangedArgs { activeBuilding = activeBuildingType});
    }

}
