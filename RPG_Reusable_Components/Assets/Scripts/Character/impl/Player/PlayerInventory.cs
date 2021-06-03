public class PlayerInventory : CharacterInventory
{
    public override void Awake()
    {
        maxInventorySize = 28;
        base.Awake();
    }
}
