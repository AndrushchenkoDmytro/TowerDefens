using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostSprite;
    private BuildingsTypeSo activeBuilding;
    private ProductionPerformanceOverlay performanceOverlay;
    void Awake()
    {
        ghostSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        performanceOverlay = GetComponent<ProductionPerformanceOverlay>();
    }

    private void Start()
    {
        PLayerController.Instance.OnActiveBuildingChanged += ((sender,e) =>
        {
            if(e.activeBuilding == null)
            {
                Hide();
            }
            else
            {
                Show(e.activeBuilding);
            }
        });
    }

    private void Update()
    {
        transform.position = PLayerController.Instance.mousePosition;

    }

    public void Show(BuildingsTypeSo building)
    {
        gameObject.SetActive(true);
        activeBuilding = building;
        if(activeBuilding.buildingClass == BuildingClass.Production)
        {
            performanceOverlay.enabled = true;
            performanceOverlay.ShowPerformanceOverlay(activeBuilding);
        }
        else
        {
            performanceOverlay.HidePerformanceOverlay();
        }
        ghostSprite.sprite = activeBuilding.buildingSprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
