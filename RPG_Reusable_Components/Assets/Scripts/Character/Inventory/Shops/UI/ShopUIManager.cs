using UnityEngine;

public class ShopUIManager : AbstractInventoryUIManger<ShopUIManager>
{
    public override void Close()
    {
        base.Close();
        ContainmentContainer.onInventoryChanged -= OnInventoryChanged;
        ContainmentContainer = null;
    }

    public void Initialize(AbstractItemInventory sInventory, AbstractItemInventory pInventory, bool cleanChildren = false)
    {
        if (sInventory == null) return;

        //Only do this if its not a refresh
        if (!cleanChildren)
        {
            ContainmentContainer = sInventory;
            
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
