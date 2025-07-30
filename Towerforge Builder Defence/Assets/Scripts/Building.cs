using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingType buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("Building Demolish Button");
        buildingRepairButton = transform.Find("Building Repair Button");

        HideBuildingDemolishButton();
        HideBuildingRepairButton();

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthSystem.OnDamage += HealthSystemOnDamaged;
        healthSystem.OnHealed += HealthSystemOnHealed;
        healthSystem.OnDied += HealthSystemOnDied;
    }

    private void HealthSystemOnDamaged(object sender, System.EventArgs e)
    {
        ShowBuildingRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystemOnHealed(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHealth())
            HideBuildingRepairButton();
    }

    private void HealthSystemOnDied(object sender, System.EventArgs e)
    {
        Instantiate(Resources.Load<Transform>("Building Destroyed Particles"), transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
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

    private void ShowBuildingRepairButton()
    {
        if (buildingRepairButton)
            buildingRepairButton.gameObject.SetActive(true);
    }

    private void HideBuildingRepairButton()
    {
        if (buildingRepairButton)
            buildingRepairButton.gameObject.SetActive(false);
    }
}
