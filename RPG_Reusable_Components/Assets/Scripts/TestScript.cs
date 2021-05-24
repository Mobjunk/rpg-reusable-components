using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public CharacterManager character;
    public ShopOpenerManager shopManager;
    public ShopInventory shopInventory;

    public void Test()
    {
        shopManager.Interact(character);
    }

    public void Test2()
    {
        shopInventory.RemoveItem(ItemManager.Instance().ForName("Axe"));
    }
}
