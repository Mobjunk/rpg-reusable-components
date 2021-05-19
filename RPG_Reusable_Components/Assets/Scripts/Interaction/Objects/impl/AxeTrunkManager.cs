using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrunkManager : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        base.OnInteraction(characterManager);
        if (!characterManager.GetChararacterInventory().Contains(0))
        {
            ChatManager.Instance().AddMessage("You take the axe out of the stump.");
            characterManager.GetChararacterInventory().Add(0);
        } else ChatManager.Instance().AddMessage("You already have a axe, don't be greedy.");
    }
}
