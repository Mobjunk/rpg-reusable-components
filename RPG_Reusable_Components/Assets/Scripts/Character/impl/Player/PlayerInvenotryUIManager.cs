using UnityEngine;

public class PlayerInvenotryUIManager : AbstractInventoryUIManger<PlayerInvenotryUIManager>
{
    public void Initialize(AbstractItemInventory pInventory, bool cleanChildren = false)
    {
        if (pInventory == null) return;
        
        //Only do this if its not a refresh
        if (!cleanChildren)
        {
            ContainmentContainer = pInventory;
            ContainmentContainer.onInventoryChanged += OnInventoryChanged;
        }
        else foreach (Transform child in InventoryContainer) Destroy(child.gameObject);
        
        SetupContainer();
    }

    private void OnInventoryChanged()
    {
        Initialize(ContainmentContainer, true);
    }
}
