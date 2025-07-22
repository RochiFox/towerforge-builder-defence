using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingType buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("Building Demolish Button");
        HideBuildingDemolishButton();

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDied += HealthSystemOnDied;
    }

    private void HealthSystemOnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishButton();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishButton();
    }

    private void ShowBuildingDemolishButton()
    {
        if (buildingDemolishButton)
            buildingDemolishButton.gameObject.SetActive(true);
    }

    private void HideBuildingDemolishButton()
    {
        if (buildingDemolishButton)
            buildingDemolishButton.gameObject.SetActive(false);
    }
}
