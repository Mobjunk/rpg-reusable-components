using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop")]
public class ShopStock : ScriptableObject
{
    public string shopName;
    public NpcData linkedNpc;
    public List<ItemData> items = new List<ItemData>();
}
