using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        base.OnInteraction(characterManager);
        Debug.Log("Tree Manager debug...");
        
        characterManager.SetAction(new WoodcuttingManager(characterManager, gameObject, GetObjectData()));
    }
}
