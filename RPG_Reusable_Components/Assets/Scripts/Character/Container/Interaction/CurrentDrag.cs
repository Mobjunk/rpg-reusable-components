using System;
using UnityEngine;

public class CurrentDrag : Singleton<CurrentDrag>
{
    private CharacterManager characterManager;
    
    [SerializeField] private ContainerManager inventoryManager;

    private void Awake()
    {
        //TODO: Find a better way of doing this...
        characterManager = GameObject.Find("Character").GetComponent<CharacterManager>();
        Player player = (Player) characterManager;
        player.InventoryManager = inventoryManager;
        player.OnCompletion();
    }

    DragHandler currentDragHangler;

    /// <summary>
    /// The current drag handler script
    /// </summary>
    /// <returns></returns>
    public DragHandler GetCurrentDragHandler()
    {
        return currentDragHangler;
    }

    /// <summary>
    /// Handles resetting the current drag script
    /// </summary>
    public void Reset()
    {
        currentDragHangler?.ResetDrag();
        SetCurrentDrag(null);
    }

    /// <summary>
    /// Handles setting what drag script is currently being used
    /// </summary>
    /// <param name="dragHandler">The script drag handler script</param>
    public void SetCurrentDrag(DragHandler dragHandler)
    {
        currentDragHangler = dragHandler;
    }
}
