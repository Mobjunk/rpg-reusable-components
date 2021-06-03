using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WoodcuttingManager : HarvestSkillManager
{
    private GameObject interactedObject;
    private ObjectData objectData;

    public WoodcuttingManager(CharacterManager characterManager, GameObject interactedObject, ObjectData objectData) : base(characterManager)
    {
        this.interactedObject = interactedObject;
        this.objectData = objectData;
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.WOODCUTTING;
    }

    public override string StartMessage()
    {
        return "You swing your axe at the tree.";
    }

    public override int EquipmentId()
    {
        return 155;
    }

    public override int TimeRequired()
    {
        return 2;
    }

    public override bool HasRequirements()
    {
        if (!CharacterManager.Inventory.HasItem(ItemManager.Instance().ForName("Axe")) && CharacterManager.GetCharacterEquipmentManager().GetEquipmentSlots()[(int)EquipmentSlots.mainhand - 1].itemId != EquipmentId())
        {
            ChatManager.Instance().AddMessage("You need an axe to chop this tree.");
            return false;
        }
        return true;
    }

    public override void ReceiveItem()
    {
        CharacterManager.Inventory.AddItem(ItemManager.Instance().ForName("Logs"));
        ObjectManager.Instance().ReplaceGameObject(interactedObject, objectData.secondaryObject, 5);
        CharacterManager.SetAction(null);
        Utility.AddSceneIfNotLoaded("DLC");
    }

    public override bool Successful()
    {
        int randomRoll = Random.Range(0, 100);
        Debug.LogError("randomRoll: " + randomRoll);
        return randomRoll >= 75;
    }
}
