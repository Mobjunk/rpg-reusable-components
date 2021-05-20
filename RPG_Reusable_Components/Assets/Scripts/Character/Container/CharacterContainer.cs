using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharacterContainer
{
    private ContainerManager containerManager;
    
    /// <summary>
    /// An array of all the items in the container
    /// </summary>
    public ItemData[] items;

    /// <summary>
    /// Initialize the container
    /// </summary>
    /// <param name="size">The size of the container</param>
    public CharacterContainer(ContainerManager containerManager, int size)
    {
        this.containerManager = containerManager;
        //Sets the array to the correct size
        items = new ItemData[size];
        
        //Fills array with empty item data
        for (int index = 0; index < size; index++)
            items[index] = new ItemData();
    }

    /// <summary>
    /// Handles swapping items from one slot to another slot
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void Swap(int from, int to)
    {
        //Creates an copy of the item
        var temp = Get(from);
        
        //Spawns around the 2 items
        items[from] = Get(to);
        items[to] = temp;
        
        UpdateUI();
    }

    /// <summary>
    /// Grabs the item for a slot
    /// </summary>
    /// <param name="slot">The slot your trying to get the item for</param>
    /// <returns>Grabs an item based on a slot</returns>
    public ItemData Get(int slot)
    {
        return items[slot];
    }
    
    /// <summary>
    /// Checks if the player has the required item
    /// </summary>
    /// <param name="itemId">The item id</param>
    /// <param name="amount">The amount the player has to have</param>
    /// <returns>If the player has the required items</returns>
    public bool Contains(int itemId, int amount = 1)
    {
        return items.Where(i => i != null && i.itemData != null).Any(i => i.itemData.itemId == itemId && i.amount >= amount);
    }

    /// <summary>
    /// Checks if the player has all the items in he list
    /// </summary>
    /// <param name="itemsData">A list of all the items required</param>
    /// <returns>Checks if the player has all the items in he list</returns>
    public bool ContainsAll(List<ItemData> itemsData)
    {
        return itemsData.All(item => Contains(item.itemData.itemId, item.amount));
    }

    /// <summary>
    /// Grabs the slot based on an item id
    /// </summary>
    /// <param name="id">The item id</param>
    /// <returns>An slot based on item id</returns>
    public int GetSlot(int id)
    {
        //Loops though all the items
        for (var index = 0; index < items.Length; index++)
        {
            //Checks if the itemdata or item is null
            if (items[index] == null || items[index].itemData == null) continue;
            //Checks if the ids match
            if (items[index].itemData.itemId == id) return index;
        }
        return -1;
    }

    /// <summary>
    /// Grabs the amount of items you have in a certain slot
    /// </summary>
    /// <param name="slot">The slot your trying to grab the amount for</param>
    /// <returns>Grabs the amount of items you have in a certain slot</returns>
    public int GetAmount(int slot)
    {
        return (from i in items where i != null && !i.itemData where Get(slot).itemData.itemId == i.itemData.itemId select i.amount).Sum();
    }

    /// <summary>
    /// Grabs the amount of items you have of a certain item id
    /// </summary>
    /// <param name="id">The item id your looking up</param>
    /// <returns>Grabs the amount of items you have of a certain item id</returns>
    public int GetAmountFromItem(int id)
    {
        return (from i in items where i != null && i.itemData != null where id == i.itemData.itemId select i.amount).Sum();
    }

    /// <summary>
    /// Grabs the next free available slot
    /// </summary>
    /// <returns>Grabs the next free available slot</returns>
    public int FreeSlot()
    {
        //Loops though all the items
        //And checks if there is no item on that slot
        for (var index = 0; index < items.Length; index++)
            if (items[index] == null || (items[index].itemData == null || items[index].itemData.itemId == -1)) return index;
        return -1;
    }

    /// <summary>
    /// Checks how many free slots the player has in the container
    /// </summary>
    /// <returns>Checks how many free slots the player has in the container</returns>
    public int FreeSlots()
    {
        return items.Count(i => i == null || i.itemData == null);
    }

    /// <summary>
    /// Handles adding an item to the ontainer
    /// </summary>
    /// <param name="itemId">The item id your trying to add</param>
    /// <param name="amount">The amount of the item you want</param>
    /// <param name="slot">The preferred slot you want to put the item in</param>
    /// <returns></returns>
    public bool Add(AbstractItemData itemToAdd, int amount = 1, int slot = -1)
    {
        if (itemToAdd == null) return false;
        
        //Grabs the new slot your trying to add the item to
        //Checks if the item is stackable and has the item to grab the slot of that item
        //Also checks if the slot is -1
        var newSlot = slot == -1 ? FreeSlot() : slot;
        if (itemToAdd.stackable && Contains(itemToAdd.itemId)) newSlot = GetSlot(itemToAdd.itemId);
        if (newSlot == -1) return false;

        //Checks if the item is stackable
        if (itemToAdd.stackable)
        {
            //Grabs the item for the lot
            var item = items[newSlot];
            //Checks if the item inside the data is null and sets the new item
            if (item.itemData == null) item.itemData = itemToAdd;
            
            //Grabs the updated total
            //Checks if the new amount is higher then the max in or under 1
            var totalAmount = item.amount + amount;
            if (totalAmount >= int.MaxValue || totalAmount < 1) return false;
            
            //Update the amount of the selected item
            item.amount = totalAmount;
            UpdateUI();
            return true;
        }
        else
        {
            //Grabs the amount of free slots
            int openSlots = FreeSlots();
            //Checks if the amount is higher then the free slots
            if (amount > openSlots) amount = openSlots;
            
            //Checks if the open slots is the same or higher then amount
            if (openSlots >= amount)
            {
                //Loops though the amount of items its has to add
                for (var index = 0; index < amount; index++)
                {
                    //If it isn't the first slot grab the next free available slot
                    if (index > 0) newSlot = FreeSlot();
                    //Grabs the item for the slot and if its null set the item
                    var item = items[newSlot];
                    if (item.itemData == null) item.itemData = itemToAdd;
                    
                    //Update the item amount
                    item.amount = 1;
                }
                UpdateUI();
                return true;
            }
        }
        
        UpdateUI();
        return true;
    }

    /// <summary>
    /// Handles removing items from the container
    /// </summary>
    /// <param name="itemId">The item id your trying to remove</param>
    /// <param name="amount">The amount your trying to remove</param>
    /// <param name="preferredSlot">The preferred slot you want to remove the slot from</param>
    /// <returns>Handles removing items from the container</returns>
    public bool Remove(AbstractItemData itemToRemove, int amount = 1, int preferredSlot = -1)
    {
        if (itemToRemove == null) return false;
        
        //Grabs the slot for the item id
        //Checks if the slot isn't -1
        var slot = GetSlot(itemToRemove.itemId);
        if (slot == -1) return false;
        
        //Grabs the item for the slot
        var item = items[slot];

        //Checks if the item is stackable
        if (itemToRemove.stackable)
        {
            //Checks if the item data or item is null
            if (item == null || item.itemData == null) return false;
            //Checks if the item amount is higher then the amount your trying to remove
            if (item.amount > amount)
                item.amount -= amount;
            else
            {
                //Sets the item to null and update the item amount
                item.itemData = null;
                item.amount = 0;
            }
        }
        else
        {
            //Loops though the amount of items your trying to remove
            for (var index = 0; index < amount; index++)
            {
                //Updates the slot
                slot = GetSlot(itemToRemove.itemId);
                //If its the first item and it has a preferred slot remove that instead
                if (index == 0 && preferredSlot != -1)
                {
                    //Grabs the item for the preferred slot
                    //And if the ids match set that slot
                    var inSlot = Get(preferredSlot);
                    if (inSlot.itemData.itemId == itemToRemove.itemId) slot = preferredSlot;
                }
                //If the slot isn't -1 set the data to null
                if (slot != -1)
                {
                    item = items[slot];
                    item.itemData = null;
                    item.amount = 0;
                }
            }
            UpdateUI();
            return true;
        }
        
        UpdateUI();
        return true;
    }

    /// <summary>
    /// Handles updating the container
    /// </summary>
    /// <param name="ui"></param>
    public void UpdateUI()
    {
        if (containerManager == null)
        {
            Debug.LogError("Cannot update UI for a npc...");
            return;
        }

        if (items.Length > containerManager.containerSlots.Count)
        {
            Debug.LogError($"Item array size is bigger then the container list [items.Length: {items.Length}, containerManager.containerSlots.Count: {containerManager.containerSlots.Count}]...");
            return;
        }
        
        for (int index = 0; index < items.Length; index++)
        {
            ContainerSlot slot = containerManager.containerSlots[index];
            
            if (items[index] == null || items[index].itemData == null)
            {
                slot.SetInteractableItem(null);
                slot.SetTextVisible(false);
                slot.SetSpriteVisible(false);
                continue;
            }
            
            int itemId = items[index].itemData.itemId;
            int amount = items[index].amount;

            if (itemId == -1)
            {
                slot.SetInteractableItem(null);
                slot.SetTextVisible(false);
                slot.SetSpriteVisible(false);
                continue;
            }

            AbstractItemData item = ItemManager.Instance().itemDefinition[itemId];

            if (item == null)
            {
                slot.SetInteractableItem(null);
                slot.SetTextVisible(false);
                slot.SetSpriteVisible(false);
                return;
            }
            
            if (item.stackable)
            {
                slot.SetTextVisible(true);
                slot.SetText(amount.ToString());
            }
            slot.SetSpriteVisible(true);
            slot.SetSprite(item.uiSprite);
            slot.SetInteractableItem(item);
        }
    }
}