using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;
    private Transform separateContainer;

    private void Awake()
    {
        barTransform = transform.Find("Bar");
        separateContainer = transform.Find("Separator Container");
    }

    private void Start()
    {
        ConstructHealthBarSeparators();

        healthSystem.OnDamage += HealthSystemOnDamaged;
        healthSystem.OnHealed += HealthSystemOnHealed;
        healthSystem.OnHealthAmountMaxChanged += HealthSystemOnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystemOnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystemOnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystemOnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        ConstructHealthBarSeparators();
    }

    private void ConstructHealthBarSeparators()
    {
        Transform separatorTemplate = separateContainer.Find("Separator Template");

        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in separateContainer)
        {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 1; i < healthSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separateContainer);
            separatorTransform.gameObject.SetActive(true);

            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);


        gameObject.SetActive(true);
    }
}
