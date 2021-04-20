using System;
using UnityEngine;
using static CharacterStateManager;

public class CharacterMovementManager : MonoBehaviour, ICharacterMovement
{
    private CharacterManager characterManager;
    private CharacterStateManager characterStateManager;
    private CharacterController characterController;
    private CharacterControlsManager characterControlsManager;

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
    [SerializeField] private float characterRotation;
    [SerializeField] private float characterRotationSpeed = 1;

    [Header("Jumping")]
    [SerializeField] private bool pressedJump;
    [SerializeField] private bool characterIsJumping;
    [SerializeField] private float verticalVelocity, characterVelocityY;
    [SerializeField] private float characterJumpingSpeed, characterJumpHeight = 3;
    [SerializeField] private Vector3 characterJumpingDirection;
    
    [Header("Falling")]
    [SerializeField] private float characterGravity = 9.81f;
    [SerializeField] private float groundedTimer;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        characterController = GetComponent<CharacterController>();
        characterControlsManager = GetComponent<CharacterControlsManager>();
        characterStateManager = GetComponent<CharacterStateManager>();
        //characterController.minMoveDistance = 0f;
    }

    public void Move()
    {
        if (characterManager.CharacterAction != null && !characterManager.CharacterAction.Interruptable()) return;
        
        HandleInputs();
        HandleFalling();
        HandleMovement();
    }

    private void HandleInputs()
    {
        bool pressingForwards = Input.GetKey(characterControlsManager.forwards);
        bool pressingBackwards = Input.GetKey(characterControlsManager.backwards);
        bool pressingRotateLeft = Input.GetKey(characterControlsManager.rotateLeft);
        bool pressingRotateRight = Input.GetKey(characterControlsManager.rotateRight);
        
        //Handles forwards and backwards movement
        if (pressingForwards) characterInputs.y = 1;
        if (pressingBackwards) characterInputs.y = pressingForwards ? 0 : -1;
        
        if (!pressingForwards && !pressingBackwards) characterInputs.y = 0;

        //Handles the left and right rotation of the character
        if (pressingRotateLeft) characterRotation = -1;
        if (pressingRotateRight) characterRotation = pressingRotateLeft ? 0 : 1;
        
        if (!pressingRotateLeft && !pressingRotateRight) characterRotation = 0;

        characterIsRunning = Input.GetKey(characterControlsManager.run);
        pressedJump = Input.GetKey(characterControlsManager.jumping);
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

        //Jumping & falling
        //if (pressedJump && characterController.isGrounded) HandleJumping();
        
        //States
        if (!inputsNormalized.Equals(Vector2.zero))
        {
            characterState = inputsNormalized.y > 0 ? CharacterStates.WALKING : CharacterStates.WALKING_BACKWARDS;
            if(characterIsRunning) characterState = inputsNormalized.y > 0 ? CharacterStates.RUNNING : CharacterStates.RUNNING_BACKWARDS;
        }
        if (characterIsJumping) characterState = CharacterStates.JUMPING;
        
        characterStateManager.SetCharacterState(characterState);
        
        //Velocity calculations
        //characterVelocity = (transform.forward * inputsNormalized.y + transform.right * inputsNormalized.x) * characterCurrentSpeed;
        if (!characterIsJumping) {
            characterVelocity = (transform.forward * inputsNormalized.y + transform.right * inputsNormalized.x) * characterCurrentSpeed; // + Vector3.up * characterVelocityY
            
            //Force the vertical velocity on the y
            characterVelocity.y = verticalVelocity;
        }
        /*else
        {
            characterVelocity = characterJumpingDirection * characterJumpingSpeed + Vector3.up * characterVelocityY;
        }*/
        
        //Make sure it resets the player action when moving
        if(characterState != CharacterStates.IDLE)
            characterManager.SetAction(null);
        //Moving
        characterController.Move(characterVelocity * Time.deltaTime);

        //characterIsJumping = false;
        /*if (characterController.isGrounded)
        {
            if (characterIsJumping)
            {
                Debug.Log("Set jumping false");
                characterIsJumping = false;
            }
            //characterVelocityY = 0;
        }*/
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
    
    private void HandleJumping()
    {
        if (!characterIsJumping)
        {
            Debug.Log("Set jumping");
            characterIsJumping = true;
        }

        characterJumpingDirection = (transform.forward * characterInputs.y + transform.right * characterInputs.x).normalized;

        characterJumpingSpeed = characterCurrentSpeed;

        characterVelocity.y = Mathf.Sqrt(characterGravity * characterJumpHeight);
    }
}
