using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;

        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearbyResourceAmount == 0)
            enabled = false;
        else
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax *
             (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer += timerMax;

            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        return nearbyResourceAmount;
    }

    public ResourceGeneratorData GetResourceGeneratorData() => resourceGeneratorData;

    public float GetTimerNormalized() => timer / timerMax;

    public float GetAmountGeneratorPerSecond() => 1 / timerMax;
}
