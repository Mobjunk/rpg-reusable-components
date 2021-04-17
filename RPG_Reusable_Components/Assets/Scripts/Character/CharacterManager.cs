using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterArmatureManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterAttackManager), typeof(CharacterStateManager), typeof(CharacterEquipmentManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterInputManager characterInputManager;
    [SerializeField] private CharacterAttackManager characterAttackManager;
    [SerializeField] private GameObject nameCanvas;
    [SerializeField] private TMP_Text nameText;

    private CharacterAction characterAction;

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

    /// <summary>
    /// Handles activating the UI canvas for the player name
    /// And also sets the text to the correct name
    /// </summary>
    /// <param name="name"></param>
    public void SetNameUI(string name)
    {
        nameCanvas.SetActive(true);
        nameText.text = $"{name}";
    }
}
