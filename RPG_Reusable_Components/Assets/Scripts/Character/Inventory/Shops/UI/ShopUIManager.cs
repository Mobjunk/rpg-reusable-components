using UnityEngine;

public class ShopUIManager : AbstractInventoryUIManger<ShopUIManager>
{

    private AbstractItemInventory playerInventory;

    public override void Close()
    {
        base.Close();
        ContainmentContainer.onInventoryChanged -= OnInventoryChanged;
        ContainmentContainer = null;
    }

    public void Initialize(AbstractItemInventory sInventory, AbstractItemInventory pInventory, bool cleanChildren = false)
    {
        if (sInventory == null || pInventory == null) return;


        //Only do this if its not a refresh
        if (!cleanChildren)
        {
            ContainmentContainer = sInventory;
            playerInventory = pInventory;
            ContainmentContainer.onInventoryChanged += OnInventoryChanged;
        }
        else foreach (Transform child in InventoryContainer) Destroy(child.gameObject);
        
        SetupContainer();
    }

    private void OnInventoryChanged()
    {
        Initialize(ContainmentContainer, playerInventory, true);
    }
}
