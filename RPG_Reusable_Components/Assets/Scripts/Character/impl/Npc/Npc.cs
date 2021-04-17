using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Npc : CharacterManager
{
    [SerializeField] public NpcData npcData;
    [SerializeField] public CharacterEquipmentManager characterEquipmentManager;

    private int[,] itemCombos = {
        {1, 62, 80, 81, 82, 12, 5, 19},
        {1, 64, 52, 53, 54, 71, 5, 35},
        {83, 54, 5, 85, 141, 51, 12, 73},
        {237, 238, 239, 240, 241, 242, 243, 244}
    };
    
    public NpcData GetNpcData()
    {
        return npcData;
    }
    
    public override void Awake()
    {
        base.Awake();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
        //Debug.Log("Player awake");
    }

    public override void Start()
    {
        base.Start();
        
        if (npcData.items.Count > 0)
            foreach (int itemId in npcData.items)
                characterEquipmentManager.EquipItem(itemId);
    }
}
