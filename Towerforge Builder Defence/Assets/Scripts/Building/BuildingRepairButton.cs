using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceType goldResourceType;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[] {
                new() { resourceType = goldResourceType, amount = repairCost }
            };

            if (ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                ResourceManager.Instance.SpendResources(resourceAmountCost);
                healthSystem.HealFull();
            }
            else
            {
                string tooltipMessage = "Cannot afford repair cost: ";

                foreach (ResourceAmount resourceAmount in resourceAmountCost)
                {
                    int playerAmount = ResourceManager.Instance.GetResourceAmount(resourceAmount.resourceType);
                    tooltipMessage += $"{resourceAmount.resourceType.name}: {playerAmount}/{resourceAmount.amount}";
                }

                TooltipUI.Instance.Show(tooltipMessage, new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }
}
