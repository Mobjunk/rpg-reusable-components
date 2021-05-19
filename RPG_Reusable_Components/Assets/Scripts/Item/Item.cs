using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [Header("Item Data")]
    public int itemId;
    public Sprite uiSprite;
    public string itemName;
    public string itemDescription;
    public bool isStackable;
    public GroundItem groundItem;
    [Header("Other system data")] public int fantasyId;
}