using UnityEngine;

public class Npc : CharacterManager
{
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Character")) other.GetComponent<Player>().SetInteraction(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Character")) other.GetComponent<Player>().SetInteraction(null);
    }
}
