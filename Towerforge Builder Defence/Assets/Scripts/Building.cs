using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingType buildingType;
    private HealthSystem healthSystem;

    void Awake()
    {
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
}
