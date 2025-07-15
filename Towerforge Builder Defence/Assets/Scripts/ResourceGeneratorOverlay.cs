using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private Transform barTransform;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        barTransform = transform.Find("Bar");

        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratorPerSecond().ToString("F1"));
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
