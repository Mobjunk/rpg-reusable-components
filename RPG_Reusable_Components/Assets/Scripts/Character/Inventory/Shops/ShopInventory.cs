using UnityEngine;

public class ShopInventory : AbstractItemInventory
{
    public ShopStock shopStock;
    
    public float buyRatio;
    public float sellRatio;

    public override void Start()
    {
        base.Start();

        AllowShifting = true;
        
        if (shopStock == null) return;
        
        foreach (ItemData item in shopStock.items)
            AddItem(item.itemData, item.amount);
    }

    public void PurchaseItem(AbstractItemData item, int amount = 1)
    {
        
    }

    public void SellItem(AbstractItemData item, int amount = 1)
    {
        
    }
}