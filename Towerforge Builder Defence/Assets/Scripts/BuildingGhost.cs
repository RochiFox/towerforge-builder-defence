using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGameObject = transform.Find("Sprite").gameObject;
        resourceNearbyOverlay = transform.Find("Resource Nearby Overlay").GetComponent<ResourceNearbyOverlay>();

        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManagerOnActiveBuildingTypeChanged;
    }

    private void BuildingManagerOnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (!e.activeBuildingType)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);

            if (e.activeBuildingType.hasResourceGeneratorData)
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
            else
                resourceNearbyOverlay.Hide();
        }
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
