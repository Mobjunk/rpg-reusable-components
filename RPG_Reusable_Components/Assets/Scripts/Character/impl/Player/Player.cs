using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ICharacterInput), typeof(CharacterMovementManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterAttackManager))]
public class Player : CharacterManager
{
    [SerializeField] private CharacterMovementManager characterMovementManager;
    [SerializeField] private CharacterInteractionManager characterInteractionManager;
    [SerializeField] private CharacterNameManager characterNameManager;
    [SerializeField] private string username;

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
        characterMovementManager.enabled = false;
        characterInteractionManager = GetComponent<CharacterInteractionManager>();
        CharacterAttackManager = GetComponent<CharacterAttackManager>();
        characterNameManager = GetComponent<CharacterNameManager>();
        
        characterMovementManager.enabled = false;
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

        if (controllerConnected && characterInputManager.GetType() != typeof(CharacterControllerManager)) UpdateInput<CharacterKeyboardManager, CharacterControllerManager>();
        else if (!controllerConnected && characterInputManager.GetType() != typeof(CharacterKeyboardManager)) UpdateInput<CharacterControllerManager, CharacterKeyboardManager>();
    }

    public void UpdateInput<T, Y>() where T : MonoBehaviour, ICharacterInput where Y : MonoBehaviour, ICharacterInput
    {
        Destroy(GetComponent<T>());
        characterInputManager = gameObject.AddComponent<Y>();

        SubscribeToInput();
    }
    
    public override void Start()
    {
        base.Start();

        SubscribeToInput();
    }

    void SubscribeToInput()
    {
        characterInputManager.OnCharacterMovement += characterMovementManager.Move;
        characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
        characterInputManager.OnCharacterAttack += CharacterAttackManager.Attack;
    }

    public void OnSceneLoaded2(Scene scene, LoadSceneMode mode)
    {
        characterMovementManager.enabled = true;
        
        gameObject.AddComponent<PlayerInventory>();
        Inventory = GetComponent<PlayerInventory>();
        
        var inventoryUIManager = gameObject.AddComponent<PlayerInvenotryUIManager>();
        inventoryUIManager.Initialize(Inventory);
        
        characterNameManager.SetNameUI(Username);
    }

    public void DisableMovement()
    {
        characterInputManager.OnCharacterMovement -= characterMovementManager.Move;
    }

    public void EnableMovement()
    {
        characterInputManager.OnCharacterMovement += characterMovementManager.Move;
    }
}
