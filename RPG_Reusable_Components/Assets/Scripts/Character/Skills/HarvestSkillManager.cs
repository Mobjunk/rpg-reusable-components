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
            reset();
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
        base.OnStart();
        currentMainHand = CharacterManager.GetCharacterEquipmentManager().GetEquipmentSlots()[(int)EquipmentSlots.mainhand].itemId;
        currentOffHand = CharacterManager.GetCharacterEquipmentManager().GetEquipmentSlots()[(int)EquipmentSlots.offhand].itemId;
        CharacterManager.GetCharacterEquipmentManager().EquipItem(EquipmentId());
    }

    public override void OnStop()
    {
        base.OnStop();
        reset();
    }

    public abstract int EquipmentId();
    public abstract int TimeRequired();
    public abstract bool HasRequirements();
    public abstract void ReceiveItem();
    public abstract bool Successful();

    public virtual void reset()
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
