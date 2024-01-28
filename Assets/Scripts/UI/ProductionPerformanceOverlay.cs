using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ProductionPerformanceOverlay : MonoBehaviour
{
    ResourceGenerator resourceGenerator;
    [SerializeField] private SpriteRenderer resourceLogo;
    [SerializeField] private TextMeshPro productionPerformance;
    [SerializeField] private Sprite[] logoSprites;

    private void Awake()
    {
        if(resourceGenerator == null) HidePerformanceOverlay();
    }

    private void Update()
    {
        if (resourceGenerator != null)
        {
            productionPerformance.text = string.Concat(resourceGenerator.GetProductionPerformans(transform.position).ToString(), "%");
        }
    }

    private void ChangeResourceLogo()
    {
        if(resourceGenerator.GetResourceType() == ResourceTypes.Wood)
        {
            resourceLogo.sprite = logoSprites[0];
        }
        else if (resourceGenerator.GetResourceType() == ResourceTypes.Stone)
        {
            resourceLogo.sprite = logoSprites[1];
        }
        else
        {
            resourceLogo.sprite = logoSprites[2];
        }
    }

    public void ShowPerformanceOverlay(BuildingsTypeSo buildingsTypeSo)
    {
        resourceLogo.enabled = true;
        productionPerformance.enabled = true;
        resourceGenerator = buildingsTypeSo.prefab.GetComponent<ResourceGenerator>();
        ChangeResourceLogo();
    }

    public void HidePerformanceOverlay()
    {
        resourceLogo.enabled = false;
        productionPerformance.enabled = false;
        enabled = false;

    }



}
