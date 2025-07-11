using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private Camera mainCamera;
    private BuildingTypeList buildingTypeList;
    private BuildingType activeBuildingType;

    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeList>(nameof(BuildingTypeList));
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            if (activeBuildingType)
                Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingType buildingType)
    {
        activeBuildingType = buildingType;
    }

    public BuildingType GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}