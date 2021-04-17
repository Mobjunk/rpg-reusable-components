using UnityEngine;

public abstract class CharacterAction : ICharacterAction
{
    private CharacterStates state;

    public CharacterStates State
    {
        get => state;
        set => state = value;
    }
    
    public CharacterStates GetCharacterState()
    {
        return State;
    }

    public virtual void Update()
    {
        
    }

    public virtual void OnStart()
    {
        
    }

    public virtual void OnStop()
    {
        
    }

    public virtual bool Interruptable()
    {
        return true;
    }
}
