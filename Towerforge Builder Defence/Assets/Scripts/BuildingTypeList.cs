using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type List")]
public class BuildingTypeList : ScriptableObject
{
    public List<BuildingType> list;
}