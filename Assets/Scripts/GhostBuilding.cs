using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostSprite;

    void Awake()
    {
        ghostSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
                Show(e.activeBuilding.buildingSprite);
            }
        });
    }

    private void Update()
    {
        transform.position = PLayerController.Instance.mousePosition;
    }

    public void Show(Sprite buildingSprite)
    {
        gameObject.SetActive(true);
        ghostSprite.sprite = buildingSprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
