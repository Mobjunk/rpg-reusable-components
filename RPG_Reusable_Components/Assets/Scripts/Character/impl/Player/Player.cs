using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInputManager), typeof(CharacterMovementManager))]
public class Player : CharacterManager
{
    [SerializeField] private CharacterMovementManager characterMovementManager;
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
        //characterInputManager.enabled = false;
        characterMovementManager = GetComponent<CharacterMovementManager>();
    }

    public override void Start()
    {
        base.Start();

        characterInputManager.OnCharacterMovement = characterMovementManager.Move;
        
        //Debug.Log("Player start");
    }
}
