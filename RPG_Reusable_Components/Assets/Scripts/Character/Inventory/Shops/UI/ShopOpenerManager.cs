using UnityEngine;

public class ShopOpenerManager : MonoBehaviour
{
    private ShopUIManager shopUIManager => ShopUIManager.Instance();

    public void Interact(CharacterManager characterManager)
    {
        if (shopUIManager.isOpened) CloseShop(characterManager);
        else OpenShop(characterManager);
    }

    private void OpenShop(CharacterManager characterManager)
    {
        AbstractItemInventory shopInventory = GetComponent<ShopInventory>();
        AbstractItemInventory playerInventory = characterManager.GetComponent<CharacterInventory>();
        
        shopUIManager.Open();
        shopUIManager.Initialize(shopInventory, playerInventory);
    }

    private void CloseShop(CharacterManager characterManager)
    {
        shopUIManager.Close();
    }
}
