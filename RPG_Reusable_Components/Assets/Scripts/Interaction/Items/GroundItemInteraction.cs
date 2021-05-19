using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemInteraction : InteractionManager
{
    private GroundItemManager groundItemManager => GroundItemManager.Instance();
    public override void OnInteraction(CharacterManager characterManager)
    {
        Debug.Log("Ground Item interaction debug...");
        GroundItemData groundItem = groundItemManager.ForGameObject(gameObject);

        characterManager.GetChararacterInventory().Add(groundItem.item.item.itemId, groundItem.item.amount);
        groundItemManager.Remove(gameObject);
    }
}
