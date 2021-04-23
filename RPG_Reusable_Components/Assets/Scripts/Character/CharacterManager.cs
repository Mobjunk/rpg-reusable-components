using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterNameManager), typeof(CharacterArmatureManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterAttackManager), typeof(CharacterStateManager), typeof(CharacterEquipmentManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterInputManager characterInputManager;
    [SerializeField] private CharacterAttackManager characterAttackManager;
    [SerializeField] private CharacterStateManager characterStateManager;
    
    [SerializeField] private CharacterEquipmentManager characterEquipmentManager;

    public CharacterEquipmentManager GetCharacterEquipmentManager()
    {
        return characterEquipmentManager;
    }

    [SerializeField] private CharacterContainer characterInventory;

    public CharacterContainer CharacterInventory
    {
        get => characterInventory;
        set => characterInventory = value;
    }

    public CharacterContainer GetChararacterInventory()
    {
        return characterInventory;
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
            characterInputManager.OnCharacterAttack = characterAttackManager.Attack;
        }
        else
        {
            characterInventory = new CharacterContainer(null, 28);
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
