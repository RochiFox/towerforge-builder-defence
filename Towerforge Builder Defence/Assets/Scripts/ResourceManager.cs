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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResourceTypeList resourceTypeList = Resources.Load<ResourceTypeList>(typeof(ResourceTypeList).Name);
            AddResource(resourceTypeList.list[0], 2);
        }
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmounDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceType resourceType) => resourceAmounDictionary[resourceType];
}
