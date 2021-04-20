using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStateManager : MonoBehaviour
{
    private CharacterManager characterManager;
    
    [SerializeField] Animator animator;
    [SerializeField] CharacterStates characterState = CharacterStates.IDLE;

    public void SetCharacterState(CharacterStates state)
    {
        characterState = state;
    }

    private void Start()
    {
        characterManager = GetComponent<CharacterManager>();
        animator = GetComponent<CharacterArmatureManager>().animator;
    }

    private void Update()
    {
        if(animator != null) animator.SetInteger("AnimationState", (int)characterState);
    }
}

public enum CharacterStates
{
    IDLE,
    WALKING,
    WALKING_BACKWARDS,
    RUNNING,
    RUNNING_BACKWARDS,
    JUMPING,
    ATTACKING_MELEE,
    ATTACKING_MAGIC,
    ATTACKING_RANGED,
    WOODCUTTING,
}
