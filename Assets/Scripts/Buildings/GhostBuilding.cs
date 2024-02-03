using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostSprite;
    [SerializeField] private GameObject applyButton;
    private BuildingsTypeSo activeBuilding;
    private ProductionPerformanceOverlay performanceOverlay;
    public System.Action OnApplyButtonPressed;
    void Awake()
    {
        ghostSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        performanceOverlay = GetComponent<ProductionPerformanceOverlay>();
        GameObject.Find("MainTower").GetComponent<MainTower>().OnGameOver += () => { gameObject.SetActive(false); };
    }

    private void Start()
    {
        PLayerController.instance.OnActiveBuildingChanged += ((sender,e) =>
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

    public void ShowApplyButton()
    {
        applyButton.SetActive(true);
    }

    public void ApplyPressed()
    {
        OnApplyButtonPressed?.Invoke();
        applyButton.SetActive(false);
    }
}
