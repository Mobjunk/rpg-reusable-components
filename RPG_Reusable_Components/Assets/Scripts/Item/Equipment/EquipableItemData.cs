using UnityEngine;

[CreateAssetMenu(fileName = "New equipable", menuName = "Equipable")]
[System.Serializable]
public class EquipableItemData : AbstractItemData
{
    [Header("MedievalFantasy Id")] public int fantasyId;
}
