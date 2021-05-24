using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterNameManager), typeof(CharacterArmatureManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterAttackManager), typeof(CharacterStateManager), typeof(CharacterEquipmentManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected ICharacterInput characterInputManager;
    [SerializeField] private CharacterAttackManager characterAttackManager;
    [SerializeField] private CharacterStateManager characterStateManager;
    
    [SerializeField] private CharacterInventory inventory;
    public CharacterInventory Inventory
    {
        get => inventory;
        set => inventory = value;
    }
    
    public CharacterEquipmentManager characterEquipmentManager;

    public CharacterEquipmentManager GetCharacterEquipmentManager()
    {
        return characterEquipmentManager;
    }

    private CharacterAction characterAction;

    public CharacterAction CharacterAction
    {
        get => characterAction;
        set => characterAction = value;
    }
    
    public virtual void Awake() { }
    
    public virtual void Start()
    {
        characterAttackManager = GetComponent<CharacterAttackManager>();
        characterStateManager = GetComponent<CharacterStateManager>();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();

        if (GetType() == typeof(Player))
        {
            //characterInputManager.OnCharacterAttack = characterAttackManager.Attack;
        }
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
            characterStateManager.SetCharacterState(CharacterStates.IDLE);
            characterAction.OnStop();
        }

        characterAction = action;
        characterAction?.OnStart();
    }
}
