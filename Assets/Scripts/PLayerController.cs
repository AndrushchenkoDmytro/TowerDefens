using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PLayerController : MonoBehaviour
{
    public static PLayerController Instance;

    private Vector3 mousePosition;
    private Camera mainCamera;
    public BuildingsTypeSo activeBuildingType { get; private set; }

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
        UpdateMousePosition();
        SpawnBuilding();
    }

    private void UpdateMousePosition()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
    }

    private void SpawnBuilding()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
    }

}
