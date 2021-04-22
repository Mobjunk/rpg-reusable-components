using UnityEngine;

[System.Serializable]
public class EquipmentSlot {
    public ItemInformation item;
    public EquipmentSlots slot;
    public Transform container;
    public GameObject instancedObject;
    public GameObject activeObject; // male or female variation
    public bool inUse;
    public int itemId;
}

