using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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

    private Npc npcInteracting;


    public void SetInteraction(Npc npc)
    {
        npcInteracting = npc;
    }

    public Npc GetInteraction()
    {
        return npcInteracting;
    }

    /// <summary>
    /// The GameObject of the item the player selected
    /// </summary>
    public GameObject selectedItem { get; set; }

    /// <summary>
    /// Handles resetting the item the player selected
    /// </summary>
    public void ResetSelected()
    {
        //Checks if there is no selected item
        if (selectedItem == null) return;
        //Grabs the slot object, canvas and image object from the selected item
        var selectedCanvas = selectedItem.transform.GetChild(0).gameObject;
        var selectedImage = selectedCanvas.transform.GetChild(0).gameObject;
        //Grabs the outline of the already selected item
        var selectedOutline = selectedImage.GetComponent<Outline>();
        //Sets the outline to false
        selectedOutline.enabled = false;
    }

    public override void Awake()
    {
        base.Awake();
        
        characterInputManager = GetComponent<ICharacterInput>();
        characterMovementManager = GetComponent<CharacterMovementManager>();
        characterMovementManager.enabled = false;
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

    public void OnCompletion()
    {
        if(inventoryManager == null) Debug.Log("21312321312321312321321321321");
        CharacterInventory = new CharacterContainer(inventoryManager, 28);
        characterMovementManager.enabled = true;
    }

    void SubscribeToInput()
    {
        characterInputManager.OnCharacterMovement += characterMovementManager.Move;
        characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
    }
}
