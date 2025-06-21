using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingType : ScriptableObject
{
    public string nameString;
    public Transform prefab;
}