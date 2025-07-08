using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type")]
public class BuildingType : ScriptableObject
{
    public string nameString;
    public Transform prefab;
}