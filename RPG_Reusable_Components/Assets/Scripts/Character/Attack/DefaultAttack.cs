using UnityEngine;

[System.Serializable]
public abstract class DefaultAttack : CharacterAction
{
    private float timePassedBy;
    private CharacterArmatureManager armatureManager;
    
    public DefaultAttack(CharacterManager characterManager) : base(characterManager)
    {
        armatureManager = characterManager.GetComponent<CharacterArmatureManager>();
    }

    public override void Update()
    {
        base.Update();
        
        timePassedBy += Time.deltaTime;
        if (timePassedBy > TimeRequired())
        {
            CharacterManager.SetAction(null);
        }
    }

    public virtual float TimeRequired()
    {
        return armatureManager.animator.GetCurrentAnimatorStateInfo(0).length;
    }
}