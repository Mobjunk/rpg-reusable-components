using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestSkillManager : CharacterAction
{
    public HarvestSkillManager(CharacterManager characterManager) : base(characterManager) { }

    private float timePassedBy;
    
    public override void Update()
    {
        base.Update();
        
        Debug.Log("Woodcutting update....");
        
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

    public override void OnStop()
    {
        base.OnStop();
        reset();
    }
    public abstract int TimeRequired();
    public abstract bool HasRequirements();
    public abstract void ReceiveItem();
    public abstract bool Successful();

    public virtual void reset()
    {
        
    }

}
