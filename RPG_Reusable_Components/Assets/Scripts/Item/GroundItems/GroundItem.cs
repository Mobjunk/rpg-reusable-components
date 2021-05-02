using UnityEngine;

[CreateAssetMenu(fileName = "New Ground item", menuName = "Ground Item")]
[System.Serializable]
public class GroundItem : ScriptableObject
{
    [Header("Item")]
    public Item item;
    [Header("Prefab")]
    public GameObject prefab;
}