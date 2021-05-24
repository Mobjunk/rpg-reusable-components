[System.Serializable]
public class ItemData
{
    public AbstractItemData itemData;
    public int amount;

    public ItemData()
    {
        itemData = null;
        amount = 0;
    }
    
    public ItemData(AbstractItemData itemData)
    {
        this.itemData = itemData;
        this.amount = 1;
    }

    public ItemData(AbstractItemData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }
}