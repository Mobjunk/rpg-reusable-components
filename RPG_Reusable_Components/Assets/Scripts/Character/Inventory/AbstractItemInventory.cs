using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class AbstractItemInventory : MonoBehaviour
{
    /// <summary>
    /// Handles the inventory changing event
    /// </summary>
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged = delegate {  };
    void InventoryChanged() => onInventoryChanged.Invoke();
    
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    public bool debugInventory = true;
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    private bool allowShifting = false;

    public bool AllowShifting
    {
        set => allowShifting = value;
    }

    /// <summary>
    /// Max amount of slots within this inventory
    /// </summary>
    public int maxInventorySize;
    /// <summary>
    /// Stack type of the inventory
    /// </summary>
    public StackType stackType = StackType.STANDARD;
    /// <summary>
    /// Array of all the items
    /// </summary>
    public ItemData[] items;

    public virtual void Awake()
    {
        items = new ItemData[maxInventorySize];
    }

    public virtual void Start()
    {
        for (int index = 0; index < maxInventorySize; index++)
            items[index] = new ItemData();
    }

    public void Swap(int from, int to)
    {
        //Creates an copy of the item
        var temp = items[from];
        
        //Spawns around the 2 items
        items[from] = items[to];
        items[to] = temp;
        
        InventoryChanged();
    }

    public void AddItem(AbstractItemData item, int itemAmount = 1)
    {
        if (!ItemFitsInventory())
        {
            if(debugInventory) Debug.LogError($"There is no room for the item[{item.name}].");
            return;
        }
        var newSlot = GetFreeSlot();
        if (item.stackable && HasItem(item)) newSlot = GetSlot(item);

        if (newSlot == -1)
        {
            if(debugInventory) Debug.LogError($"No slot to add the item[{item.name}].");
            return;
        }
        
        if (item.stackable || stackType.Equals(StackType.ALWAYS_STACK))
        {
            ItemData currentItem = items[newSlot];
            if (currentItem.itemData == null) currentItem.itemData = item;
            
            var totalAmount = currentItem.amount + itemAmount;
            if (totalAmount >= int.MaxValue || totalAmount < 1)
            {
                if(debugInventory) Debug.LogError($"Total amount is higher then max int or amount is 0.");
                return;
            }
            
            currentItem.amount = totalAmount;
        }
        else
        {
            for (int index = 0; index < itemAmount; index++)
            {
                int freeSlot = GetFreeSlot();
                if (freeSlot == -1)
                {
                    if(debugInventory) Debug.LogError("No free slots were found.");
                    return;
                }
                items[freeSlot] = new ItemData(item);
            }
        }
        InventoryChanged();
    }

    public void RemoveItem(AbstractItemData item, int itemAmount = 1)
    {
        int slot = GetSlot(item);
        if (slot == -1)
        {
            if(debugInventory) Debug.LogError($"There is no slot found with this item[{item.name}].");
            return;
        }

        ItemData currentItem = items[slot];
        bool shiftContainer = false;
        if (currentItem == null)
        {
            if(debugInventory) Debug.Log($"There is currently no item in slot[{slot}].");
            return;
        }

        if (item.stackable || stackType.Equals(StackType.ALWAYS_STACK))
        {
            if (currentItem.amount > itemAmount) currentItem.amount -= itemAmount;
            else
            {
                currentItem.itemData = null;
                currentItem.amount = 0;
                shiftContainer = true;
            }
        }
        else
        {
            for (int index = 0; index < itemAmount; index++)
            {
                slot = GetSlot(item);
                if (slot != -1)
                {
                    currentItem = items[slot];
                    currentItem.itemData = null;
                    currentItem.amount = 0;
                } else if(debugInventory) Debug.LogError($"There is no item[{item.name}] to remove.");
            }
        }

        if(allowShifting && shiftContainer) Shift();
        
        InventoryChanged();
    }

    public bool HasItem(AbstractItemData item, int itemAmount = 1)
    {
        return items.Any(data => data.itemData == item && data.amount >= itemAmount);
    }

    public bool ItemFitsInventory()
    {
        return items.Any(data => data.itemData == null);
    }

    int GetSlot(AbstractItemData item)
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].itemData == item) return index;
        return -1;
    }

    int GetFreeSlot()
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].itemData == null) return index;
        return -1;
    }
    
    public void Shift() {
        ItemData[] old = items;
        items = new ItemData[maxInventorySize];
        int newIndex = 0;
        for (int i = 0; i < items.Length; i++) {
            if (old[i].itemData != null) {
                items[newIndex] = old[i];
                newIndex++;
            }
        }
        InventoryChanged();
    }
}

public enum StackType
{
    STANDARD,
    ALWAYS_STACK
}