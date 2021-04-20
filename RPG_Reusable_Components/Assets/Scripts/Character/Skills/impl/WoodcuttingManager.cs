using UnityEngine;

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

    public override void OnStart()
    {
        if (!HasRequirements())
        {
            //TODO: Do something...
            return;
        }
        
        Debug.Log("Start woodcutting...");
    }

    public override int TimeRequired()
    {
        return 2;
    }

    public override bool HasRequirements()
    {
        return true;
    }

    public override void ReceiveItem()
    {
        Debug.Log("Receive item...");
        
        ObjectManager.Instance().ReplaceGameObject(interactedObject, objectData.secondaryObject, 5);
        CharacterManager.SetAction(null);
    }

    public override bool Successful()
    {
        return true;
    }
}
