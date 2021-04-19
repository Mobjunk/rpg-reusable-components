using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterNameManager), typeof(CharacterArmatureManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterAttackManager), typeof(CharacterStateManager), typeof(CharacterEquipmentManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterInputManager characterInputManager;
    [SerializeField] private CharacterAttackManager characterAttackManager;

    private CharacterAction characterAction;
    
    public virtual void Awake() { }
    
    public virtual void Start()
    {
        characterAttackManager = GetComponent<CharacterAttackManager>();
        
        if(GetType() == typeof(Player))
            characterInputManager.OnCharacterAttack = characterAttackManager.Attack;
    }

    public virtual void Update()
    {
        characterAction?.Update();
    }

    public void SetAction(CharacterAction action)
    {
        if (characterAction != null)
        {
            if (!characterAction.Interruptable()) return;
            characterAction.OnStop();
        }

        characterAction = action;
        characterAction?.OnStart();
    }
}
