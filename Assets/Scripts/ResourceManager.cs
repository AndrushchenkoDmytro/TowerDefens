using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    /*public class ResourceChangedEventArgs : EventArgs
    {
        public Resource ResourceType { get; set; }
        public int AmountChanged { get; set; }
    }*/

    private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();

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
        resources.Add(Resource.Wood, 0);
        resources.Add(Resource.Stone, 0);
        resources.Add(Resource.Gold, 0);
    }

    public void AddResource(Resource type, int value)
    {
        resources[type] += value;
        OnResourcesAmountChanged?.Invoke(this,EventArgs.Empty); //new ResourceChangedEventArgs { ResourceType = type, AmountChanged = value }
    }

    public int GetResource(Resource type)
    {
        return resources[type];
    }

}


public enum Resource
{
    Wood,
    Stone,
    Gold
}