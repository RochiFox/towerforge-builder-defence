using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingType buildingType;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        timerMax = buildingType.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer += timerMax;

            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
        }
    }
}
