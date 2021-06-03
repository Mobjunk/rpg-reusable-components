using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackManager : MonoBehaviour
{
    private CharacterManager characterManager;
    private CharacterEquipmentManager characterEquipmentManager;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
    }

    public virtual void Attack()
    {
        EquipmentSlot mainHand = characterEquipmentManager.GetEquipmentSlots()[(int) EquipmentSlots.mainhand - 1];
        EquipmentSlot offHand = characterEquipmentManager.GetEquipmentSlots()[(int) EquipmentSlots.offhand - 1];
        
        Debug.Log($"mainHand: {mainHand}, offHand: {offHand}");
        if (mainHand.itemId != 0 || offHand.itemId != 0)
        {
            DefaultAttack attack = null;
            if (mainHand.itemId != 0)
            {
                AttackType attackType = GetAttackType(mainHand.itemId);
                attack = GetAttack(attackType);
            } else if (offHand.itemId != 0)
            {
                AttackType attackType = GetAttackType(offHand.itemId);
                attack = GetAttack(attackType);
            }
            
            if(attack != null) characterManager.SetAction(attack);
        }
    }

    private AttackType GetAttackType(int itemId)
    {
        EquipableItemData item = (EquipableItemData) ItemManager.Instance().ForId(itemId);
        if (item != null) return item.attackType;
        return AttackType.NONE;
    }

    private DefaultAttack GetAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.MELEE:
                return new MeleeAttack(characterManager);
        }
        return null;
    }
    
}
