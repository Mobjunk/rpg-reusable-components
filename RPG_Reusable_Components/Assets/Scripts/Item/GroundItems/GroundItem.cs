using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Ground item", menuName = "Ground Item")]
[System.Serializable]
public class GroundItem : ScriptableObject
{
    [Header("Item")]
    public Item item;
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Settings")]
    public bool needsCollider;
    public bool updateScale;
    public bool updateRotation;
    public float sizeY;
}