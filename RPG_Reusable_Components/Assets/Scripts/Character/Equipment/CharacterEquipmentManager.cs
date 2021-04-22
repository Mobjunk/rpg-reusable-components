using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterEquipmentManager : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance();
    private CharacterArmatureManager characterArmatureManager;
    private CharacterDesignManager characterDesignManager;
    
    [SerializeField] List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

    private void Awake()
    {
        characterArmatureManager = GetComponent<CharacterArmatureManager>();
        characterDesignManager = GetComponent<CharacterDesignManager>();
    }

    public List<EquipmentSlot> GetEquipmentSlots()
    {
        return equipmentSlots;
    }

    public EquipmentSlot GetEquipmentSlot(EquipmentSlots slot)
    {
        return equipmentSlots.FirstOrDefault(e => e.slot == slot);
    }
    
    public void UnloadSlot(EquipmentSlots slot) {
        if (GetEquipmentSlot(slot).inUse) {
            GetEquipmentSlot(slot).inUse = false;
            Destroy(GetEquipmentSlot(slot).instancedObject);
        }
    }
    
    public void UnEquipAll() {
        foreach (EquipmentSlot s in equipmentSlots) {
            if (s.inUse) Destroy(s.instancedObject);
            s.inUse = false;
        }
        //TODO: Find something
        //if(itemManager.loadAllItemsMode == false)
        //    characterArmatureManager.SetupBody();
    }
    
    public ItemInformation EquipItem(int itemId) { // use this to equip items
        ItemInformation item = itemManager.LoadItem(characterArmatureManager, characterDesignManager, this, itemId);
        //TODO: Find something
        //if(loadAllItemsMode==false)
        characterArmatureManager.SetupBody();
        return item;
    }

    public void UnEquipItem(int itemId)
    {
        foreach (var s in equipmentSlots.Where(s => s.itemId == itemId))
        {
            UnequipSlot(s.slot);
        }
    }
    
    public void UnEquipItem(ItemInformation item) {
        UnequipSlot(item.equipmentSlot);
    }
    
    public bool HasItemEquipped(int itemId)
    {
        return equipmentSlots.Any(s => s.itemId == itemId);
    }
    
    public void UnequipSlot(EquipmentSlots slot) {
        UnloadSlot(slot);
        characterArmatureManager.SetupBody();
    }
}
