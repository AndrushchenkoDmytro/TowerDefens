using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    /*public class ResourceChangedEventArgs : EventArgs
    {
        public Resource ResourceType { get; set; }
        public int AmountChanged { get; set; }
    }*/

    private Dictionary<ResourceTypes, int> resources = new Dictionary<ResourceTypes, int>();

    public event EventHandler OnResourcesAmountChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeResources();
    }

    private void InitializeResources()
    {
        resources.Add(ResourceTypes.Wood, 60);
        resources.Add(ResourceTypes.Stone, 0);
        resources.Add(ResourceTypes.Gold, 0);
    }

    public void AddResource(ResourceTypes type, int value)
    {
        resources[type] += value;
        OnResourcesAmountChanged?.Invoke(this,EventArgs.Empty); //new ResourceChangedEventArgs { ResourceType = type, AmountChanged = value }
    }

    public int GetResource(ResourceTypes type)
    {
        return resources[type];
    }

    public bool CanAfford(PriceList pricelist)
    {
        if (resources[ResourceTypes.Wood] < pricelist.wood) { ToolTips.Instance.ShowNotEnoughResourcesTip(); return false; }
        if (resources[ResourceTypes.Stone] < pricelist.stone) { ToolTips.Instance.ShowNotEnoughResourcesTip(); return false; }
        if (resources[ResourceTypes.Gold] < pricelist.gold) { ToolTips.Instance.ShowNotEnoughResourcesTip(); return false; }
        MakePurchase(pricelist);
        return true;
    }

    private void MakePurchase(PriceList pricelist)
    {
        resources[ResourceTypes.Wood] -= pricelist.wood;
        resources[ResourceTypes.Stone] -= pricelist.stone;
        resources[ResourceTypes.Gold] -= pricelist.gold;
        OnResourcesAmountChanged?.Invoke(this, EventArgs.Empty);
    }

}


public enum ResourceTypes
{
    Wood,
    Stone,
    Gold
}
