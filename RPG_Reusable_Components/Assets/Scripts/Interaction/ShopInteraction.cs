using UnityEngine;

public class ShopInteraction : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        ShopOpenerManager shopManager = GetComponent<ShopOpenerManager>();
        if (shopManager == null)
        {
            Debug.LogError("Shop manager is null");
            return;
        }
        shopManager.Interact(characterManager);
    }
}