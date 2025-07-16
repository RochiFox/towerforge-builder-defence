using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Resource Type")]
public class ResourceType : ScriptableObject
{
    public string nameString;
    public string nameShort;
    public Sprite sprite;
    public string colorHex;
}
