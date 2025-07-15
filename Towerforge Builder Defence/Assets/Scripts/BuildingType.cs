using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type")]
public class BuildingType : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
}