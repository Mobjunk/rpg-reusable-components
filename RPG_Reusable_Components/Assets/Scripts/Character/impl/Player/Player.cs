using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(ICharacterInput), typeof(CharacterMovementManager), typeof(CharacterInteractionManager))]
public class Player : CharacterManager
{
    [SerializeField] private CharacterMovementManager characterMovementManager;
    [SerializeField] private CharacterInteractionManager characterInteractionManager;
    [SerializeField] private string username;
    [SerializeField] private ContainerManager inventoryManager;

    public ContainerManager InventoryManager
    {
        get => inventoryManager;
        set => inventoryManager = value;
    }

    public string Username
    {
        get => username;
        set => username = value;
    }
    
    public override void Awake()
    {
        base.Awake();
        
        characterInputManager = GetComponent<ICharacterInput>();
        characterMovementManager = GetComponent<CharacterMovementManager>();
        //characterMovementManager.enabled = false;
        characterInteractionManager = GetComponent<CharacterInteractionManager>();
    }

    public override void Update()
    {
        base.Update();
        
        bool controllerConnected = false;
        foreach(string name in Input.GetJoystickNames())
        {
            //Debug.Log("Controllername: " + name);
            switch (name)
            {
                case "Controller (Xbox 360 Wireless Receiver for Windows)":
                    controllerConnected = true;
                    break;
            }
        }

        if (controllerConnected && characterInputManager.GetType() != typeof(CharacterControllerManager))
        {
            Destroy(GetComponent<CharacterKeyboardManager>());
            characterInputManager = gameObject.AddComponent<CharacterControllerManager>();

            SubscribeToInput();
        } else if (!controllerConnected && characterInputManager.GetType() != typeof(CharacterKeyboardManager))
        {
            Destroy(GetComponent<CharacterControllerManager>());
            characterInputManager = gameObject.AddComponent<CharacterKeyboardManager>();

            SubscribeToInput();
        }
    }

    public override void Start()
    {
        base.Start();

        SubscribeToInput();
    }

    public void OnCompletion()
    {
        //characterMovementManager.enabled = true;
        CharacterInventory = new CharacterContainer(inventoryManager, 28);
    }

    void SubscribeToInput()
    {
        characterInputManager.OnCharacterMovement += characterMovementManager.Move;
        characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
    }
}
