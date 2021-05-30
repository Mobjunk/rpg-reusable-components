using UnityEngine;

public class Npc : CharacterManager
{
    private InteractionMenuManager interactionMenu => InteractionMenuManager.Instance();
    
    [SerializeField] public NpcData npcData;
    
    public NpcData GetNpcData()
    {
        return npcData;
    }
    
    public override void Awake()
    {
        base.Awake();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
    }

    public override void Start()
    {
        base.Start();
        
        if (npcData.items.Count > 0)
            foreach (int itemId in npcData.items)
                characterEquipmentManager.EquipItem(itemId);
    }
}
