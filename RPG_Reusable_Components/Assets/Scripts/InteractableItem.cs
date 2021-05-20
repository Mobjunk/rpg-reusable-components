using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DynamicOptionMenu;

public abstract class InteractableItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private DynamicOptionMenu dynamicMenu => DynamicOptionMenu.Instance();

    private bool debug = false;
    [HideInInspector] public int slot;
    [HideInInspector] public Image image;
    [HideInInspector] public AbstractItemData interactableItem;

    public void SetInteractableItem(AbstractItemData interactableItem)
    {
        this.interactableItem = interactableItem;
    }
    
    public virtual void Start()
    {
        slot = int.Parse(name.Replace("Slot ", "").Replace("(", "").Replace(")", ""));
    }

    public void HandleLeftClick()
    {
        if(debug) Debug.Log("Left click...");
    }

    public void HandleRightClick()
    {
        if(debug) Debug.Log("Right click...");
        if (image.sprite == null || interactableItem == null) return;

        HandleAllMenus();
    }

    void HandleAllMenus()
    {
        //Checks if you clicked the slot you have opened
        if (dynamicMenu.slotOpened == slot)
        {
            dynamicMenu.Close();
            return;
        }

        //If there is already a menu opened first close the old one
        if (dynamicMenu.slotOpened != -1) dynamicMenu.Close();
        
        //Opens the dynamic option menu
        dynamicMenu.Open(interactableItem.options.ToArray(), gameObject, MenuType.ITEM, interactableItem.name);

        //Sets what slot you have opened
        dynamicMenu.slotOpened = slot;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle) HandleLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right) HandleRightClick();
    }

    public void OnPointerEnter(PointerEventData eventData) { }
    public void OnPointerExit(PointerEventData eventData) { }
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData) { }
    public void OnSelect(BaseEventData eventData) { }
}
