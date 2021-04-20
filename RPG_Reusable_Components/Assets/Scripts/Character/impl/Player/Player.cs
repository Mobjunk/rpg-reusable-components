using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInputManager), typeof(CharacterMovementManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterControlsManager))]
public class Player : CharacterManager
{
    [SerializeField] private CharacterMovementManager characterMovementManager;
    [SerializeField] private CharacterInteractionManager characterInteractionManager;
    [SerializeField] private string username;

    public string Username
    {
        get => username;
        set => username = value;
    }
    
    public override void Awake()
    {
        base.Awake();
        
        characterInputManager = GetComponent<CharacterInputManager>();
        characterMovementManager = GetComponent<CharacterMovementManager>();
        characterInteractionManager = GetComponent<CharacterInteractionManager>();
    }

    public override void Start()
    {
        base.Start();

        characterInputManager.OnCharacterMovement = characterMovementManager.Move;
        characterInputManager.OnCharacterInteraction = characterInteractionManager.OnCharacterInteraction;
    }
}
