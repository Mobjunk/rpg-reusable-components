using System;
using UnityEngine;
using static CharacterStateManager;

public class CharacterMovementManager : MonoBehaviour, IMovement
{
    private CharacterManager characterManager;
    private CharacterStateManager characterStateManager;
    private CharacterController characterController;

    [Header("Inputs")]
    [SerializeField] private Vector2 characterInputs;
    [SerializeField] private Vector3 characterVelocity;

    [Header("Walking")]
    [SerializeField] private float characterCurrentSpeed;
    [SerializeField] private float characterWalkSpeed = 2.5f;
    
    [Header("Running")]
    [SerializeField] private float characterRunSpeed = 1.5f;
    [SerializeField] private bool characterIsRunning;
    
    [Header("Rotation")]
    //TODO: Fix this so its handled in the input file
    [SerializeField] private float characterRotation;
    [SerializeField] private float characterRotationSpeed = 1;

    [Header("Jumping")]
    [SerializeField] private bool characterIsJumping;
    [SerializeField] private float verticalVelocity;
    
    [Header("Falling")]
    [SerializeField] private float characterGravity = 9.81f;
    [SerializeField] private float groundedTimer;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        characterController = GetComponent<CharacterController>();
        characterStateManager = GetComponent<CharacterStateManager>();
        //characterController.minMoveDistance = 0f;
    }

    public void Move(Vector2 characterInputs, float characterRotation, bool characterIsRunning)
    {
        if (characterManager.CharacterAction != null && !characterManager.CharacterAction.Interruptable()) return;

        this.characterInputs = characterInputs;
        this.characterRotation = characterRotation;
        this.characterIsRunning = characterIsRunning;
        
        HandleFalling();
        HandleMovement();
    }

    private void HandleMovement()
    {

        CharacterStates characterState = CharacterStates.IDLE;
        
        Vector2 inputsNormalized = characterInputs;

        //Handles speed
        characterCurrentSpeed = characterWalkSpeed;
        if (characterIsRunning) characterCurrentSpeed *= characterRunSpeed;

        //Rotations
        Vector3 rotation = transform.eulerAngles + new Vector3(0, characterRotation * characterRotationSpeed, 0);
        transform.eulerAngles = rotation;

        //States
        if (!inputsNormalized.Equals(Vector2.zero))
        {
            characterState = inputsNormalized.y > 0 ? CharacterStates.WALKING : CharacterStates.WALKING_BACKWARDS;
            if(characterIsRunning) characterState = inputsNormalized.y > 0 ? CharacterStates.RUNNING : CharacterStates.RUNNING_BACKWARDS;
        }
        if (characterIsJumping) characterState = CharacterStates.JUMPING;

        bool updateState = !(characterManager.CharacterAction != null && characterManager.CharacterAction.GetCharacterState() != characterState);

        if(updateState) characterStateManager.SetCharacterState(characterState);
        
        //Velocity calculations
        if (!characterIsJumping) {
            characterVelocity = (transform.forward * inputsNormalized.y + transform.right * inputsNormalized.x) * characterCurrentSpeed; // + Vector3.up * characterVelocityY
            
            //Force the vertical velocity on the y
            characterVelocity.y = verticalVelocity;
        }
        //Make sure it resets the player action when moving
        if(characterState != CharacterStates.IDLE)
            characterManager.SetAction(null);
        //Moving
        characterController.Move(characterVelocity * Time.deltaTime);
    }

    private void HandleFalling()
    {
        bool groundedPlayer = characterController.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }
 
        //Handles falling
        if (groundedPlayer && verticalVelocity < 0) verticalVelocity = 0f;
        verticalVelocity -= characterGravity * Time.deltaTime;
    }
}
