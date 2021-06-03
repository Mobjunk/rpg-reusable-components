using UnityEngine;

public class PlayerInvenotryUIManager : AbstractInventoryUIManger<PlayerInvenotryUIManager>
{
    public void Initialize(AbstractItemInventory pInventory, bool cleanChildren = false)
    {
        if (pInventory == null) return;

        if (ContainmentPrefab == null)
        {
            ContainmentPrefab = Resources.Load<GameObject>("Slot");
            if (ContainmentPrefab != null) Debug.Log("ContainmentPrefab is no longer null, loaded from resource folder...");
            else Debug.LogError("ContainmentPrefab is still null");
        }

        if (InventoryUI == null)
        {
            InventoryUI = GameObject.Find("Inventory Manager");
            if (InventoryUI != null) Debug.Log("InventoryUI is no longer null, found the correct gameobject...");
            else Debug.LogError("InventoryUI is still null");
        }
        
        if (InventoryContainer == null)
        {
            InventoryContainer = InventoryUI.transform;
            if (InventoryContainer != null) Debug.Log("InventoryContainer is no longer null, found the correct transform...");
            else Debug.LogError("InventoryContainer is still null");
        }
        
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
