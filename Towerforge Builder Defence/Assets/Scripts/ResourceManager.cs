using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceType, int> resourceAmounDictionary;

    public event EventHandler OnResourceAmountChanged;

    private void Awake()
    {
        Instance = this;

        resourceAmounDictionary = new Dictionary<ResourceType, int>();

        ResourceTypeList resourceTypeList = Resources.Load<ResourceTypeList>(typeof(ResourceTypeList).Name);

        foreach (ResourceType resourceType in resourceTypeList.list)
        {
            resourceAmounDictionary[resourceType] = 0;
        }
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmounDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceType resourceType) => resourceAmounDictionary[resourceType];

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {

            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            resourceAmounDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
