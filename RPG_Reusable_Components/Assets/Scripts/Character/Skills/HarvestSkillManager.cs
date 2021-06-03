using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestSkillManager : CharacterAction
{
    public HarvestSkillManager(CharacterManager characterManager) : base(characterManager) { }

    private float timePassedBy;
    private int currentMainHand;
    private int currentOffHand;
    
    public override void Update()
    {
        base.Update();
        
        if(!HasRequirements()) {
            Reset();
            return;
        }

        timePassedBy += Time.deltaTime;
        if (timePassedBy > TimeRequired())
        {
            if (Successful()) ReceiveItem();
            timePassedBy = 0;
        }
    }

    public override void OnStart()
    {
        Debug.Log("Start action...");
        base.OnStart();
        
        currentMainHand = CharacterManager.GetCharacterEquipmentManager().GetEquipmentSlots()[(int)EquipmentSlots.mainhand - 1].itemId;
        currentOffHand = CharacterManager.GetCharacterEquipmentManager().GetEquipmentSlots()[(int)EquipmentSlots.offhand - 1].itemId;
        
        if (!HasRequirements())
        {
            CharacterManager.SetAction(null);
            return;
        }

        StartMessage();
        CharacterManager.GetCharacterEquipmentManager().EquipItem(EquipmentId());
    }

    public override void OnStop()
    {
        base.OnStop();
        Reset();
    }

    public abstract string StartMessage();
    public abstract int EquipmentId();
    public abstract int TimeRequired();
    public abstract bool HasRequirements();
    public abstract void ReceiveItem();
    public abstract bool Successful();

    public virtual void Reset()
    {
        if (EquipmentId() != -1)
        {
            if(currentMainHand != 0) CharacterManager.GetCharacterEquipmentManager().EquipItem(currentMainHand);
            else CharacterManager.GetCharacterEquipmentManager().UnequipSlot(EquipmentSlots.mainhand);
            
            if(currentMainHand != 0) CharacterManager.GetCharacterEquipmentManager().EquipItem(currentOffHand);
            else CharacterManager.GetCharacterEquipmentManager().UnequipSlot(EquipmentSlots.offhand);
        }
    }

}
