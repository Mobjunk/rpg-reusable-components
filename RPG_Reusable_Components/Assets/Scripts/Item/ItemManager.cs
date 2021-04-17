using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    
    [HideInInspector] public bool loadAllItemsMode = false;

        
    public Item LoadItem(CharacterArmatureManager armatureManager, CharacterDesignManager designManager, CharacterEquipmentManager equipmentManager, int itemId) {
        return LoadItem(armatureManager, designManager, equipmentManager, itemId, ItemType.Equipment);
    }
    
    public Item LoadItem(CharacterArmatureManager armatureManager, CharacterDesignManager designManager, CharacterEquipmentManager equipmentManager, int itemId, ItemType itemType)
    {
        try
        {
            if (itemId == 0) return null;

            GameObject loadedItem = null;
            object loadedObject = Resources.Load("CustomizableCharacters/Race " + designManager.CharacterRace + "/" + itemType + "/" + itemId, typeof(GameObject));
            if (loadedObject == null)
            {
                Debug.LogError(gameObject.name + " Failed to load " + itemType + " item " + itemId + ". Make sure you are using the correct item ID!");
                return null;
            }

            loadedItem = Instantiate((GameObject) loadedObject);

            Item item = loadedItem.GetComponent<Item>();
            EquipmentSlot slot = equipmentManager.GetEquipmentSlot(item.equipmentSlot);
            GameObject itemObject = null;
            item.male.SetActive(false);
            item.female.SetActive(false);

            if (designManager.CharacterGender == 0)
            {
                itemObject = item.male;
                Destroy(item.female);
            }
            else
            {
                itemObject = item.female;
                Destroy(item.male);
            }

            itemObject.SetActive(true);
            loadedItem.name = $"{(designManager.CharacterGender == 0 ? "M" : "F")} {(item.equipmentSlot == EquipmentSlots.none ? "BodyPart" : item.equipmentSlot.ToString())} {itemId}";

            if (item.skinned)
            {
                SkinnedMeshRenderer skinnedRenderer = itemObject.GetComponentInChildren<SkinnedMeshRenderer>();
                skinnedRenderer.bones = armatureManager.GetReferenceMesh().bones;
                skinnedRenderer.updateWhenOffscreen = true;
                if (itemType == ItemType.Equipment) item.transform.SetParent(armatureManager.EquippedItemsParent);
                else item.transform.SetParent(armatureManager.BodyPartsParent);


                foreach (Transform t in skinnedRenderer.GetComponentsInParent<Transform>()[1].GetComponentInChildren<Transform>())
                    if (t.GetComponent<SkinnedMeshRenderer>() == null) Destroy(t.gameObject);
            }
            else item.transform.SetParent(slot.container);


            loadedItem.transform.localPosition = Vector3.zero;
            loadedItem.transform.localScale = new Vector3(1, 1, 1);
            loadedItem.transform.localRotation = Quaternion.identity;


            if (slot != null)
            {
                if (slot.inUse)
                    if (loadAllItemsMode == false) //replace current item, if you need to remove stats of the previous item, do it here (e.g. character health, damage)
                        Destroy(slot.instancedObject);

                slot.item = item;
                slot.itemId = itemId;
                slot.inUse = true;
                slot.instancedObject = loadedItem;
                slot.activeObject = itemObject.gameObject;
            }

            if (itemType == ItemType.BodyParts) armatureManager.GetBodyParts().Add(loadedItem);

            armatureManager.GetReferenceMesh().enabled = false;
            if (loadAllItemsMode == false) SetItemColor(designManager, itemObject.gameObject);
            return item;
        }
        catch (Exception e)
        {
            Debug.LogError($"Armature null: {(armatureManager == null ? "true" : "false")}, Design null {(designManager == null ? "true" : "false")}, " + gameObject.name + " Failed to load " + itemType + " item " + itemId + " (" + e + ")");
            return null;
        }
    }

    public void SetItemColor(CharacterDesignManager designManager, GameObject itemObject) {
        ColorChanger colorChanger = itemObject.GetComponent<ColorChanger>();
        
        if (colorChanger == null) return;
        
        foreach (ColorChanger.Changer c in colorChanger.changers) {
            Renderer r = c.rend;
            Material m = r.materials[c.materialIndex];
            if (c.type == ColorChanger.Type.Skin) m.color = designManager.CharacterSkinColor;
            else if (c.type == ColorChanger.Type.Hair) m.color = designManager.CharacterHairColor;
            else if (c.type == ColorChanger.Type.Mouth) m.color = designManager.CharacterMouthColor;
            else if (c.type == ColorChanger.Type.Eyes) m.color = designManager.CharacterEyeColor;
        }
    }
}
