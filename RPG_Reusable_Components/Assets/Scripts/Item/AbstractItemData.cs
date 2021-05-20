using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
[System.Serializable]
public class AbstractItemData : ScriptableObject
{
    public int itemId;
    public string name;
    public string description;
    public Sprite uiSprite;
    public int value;
    public bool stackable;
    public List<string> options = new List<string>();

    [Header("GroundItem Data")] public GroundItem groundItem;
}