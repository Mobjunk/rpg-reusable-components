using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrunkManager : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        base.OnInteraction(characterManager);
        if (!characterManager.Inventory.HasItem(ItemManager.Instance().ForName("Axe")))
        {
            ChatManager.Instance().AddMessage("You take the axe out of the stump.");
            characterManager.Inventory.AddItem(ItemManager.Instance().ForName("Axe"));
        } else ChatManager.Instance().AddMessage("You already have a axe, don't be greedy.");
    }
}
