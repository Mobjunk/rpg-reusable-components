using UnityEngine;

[CreateAssetMenu(fileName = "New equipable", menuName = "Equipable")]
[System.Serializable]
public class EquipableItemData : AbstractItemData
{
    [Header("MedievalFantasy Id")] public int fantasyId;
    [Header("Attack Type")] public AttackType attackType;
}

public enum AttackType
{
    NONE,
    MELEE,
    MAGIC,
    RANGED
}