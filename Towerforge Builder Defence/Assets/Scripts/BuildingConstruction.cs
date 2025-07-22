using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingType buildingType)
    {
        Transform buildingConstructionPrefab = Resources.Load<Transform>("Building Construction");

        Transform arrowTransform = Instantiate(buildingConstructionPrefab, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = arrowTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }

    private BuildingType buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());

        if (constructionTimer <= 0f)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingType buildingType)
    {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstructionTimerNormalized() => 1 - constructionTimer / constructionTimerMax;
}
