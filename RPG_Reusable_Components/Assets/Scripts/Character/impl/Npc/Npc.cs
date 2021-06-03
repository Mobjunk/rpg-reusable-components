using UnityEngine;

public class Npc : CharacterManager
{
    [SerializeField] public NpcData npcData;
    [SerializeField] private CharacterNameManager characterNameManager;
    
    public NpcData GetNpcData()
    {
        return npcData;
    }
    
    public override void Awake()
    {
        base.Awake();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
        characterNameManager = GetComponent<CharacterNameManager>();
    }

    public override void Start()
    {
        base.Start();
        
        if (npcData.items.Count > 0)
            foreach (int itemId in npcData.items)
                characterEquipmentManager.EquipItem(itemId);
        
        //characterNameManager.SetNameUI(npcData.name);
    }
}
