using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    //private CharacterManager characterManager;
    
    [SerializeField] private GameObject parent;
    [SerializeField] private bool allowDrag = true;
    
    private void Start()
    {
        //characterManager = GameObject.Find("Character").GetComponent<CharacterManager>();
        parent = transform.parent.gameObject;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged == null || !allowDrag) return;
        
        //Grabs the image object (slot your moving)
        var image = DragHandler.itemBeingDragged.gameObject;
        //Grabs the drag handler of the slot your moving
        var drag = image.GetComponent<DragHandler>();
        //The slot your dragging from
        var slot = drag.slot;

        //Converts the name of the slot to wich slot it is
        var fromSlot = int.Parse(slot.name.Replace("Slot ", "").Replace("(", "").Replace(")", ""));
        //Converts the name of the gameobject to wich slot it is
        var toSlot = int.Parse(gameObject.name.Replace("Slot ", "").Replace("(", "").Replace(")", ""));

        //Makes it so it blocks raycast again for the slot that was being dragged
        DragHandler.itemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //Grabs the transform of the slot being dragged
        var trans = DragHandler.itemBeingDragged.transform.parent;
        //Grabs the canvas of the slot being dragged
        var canvas = trans.GetComponent<Canvas>();
        //Sets the sorting layer of the slot to 1
        canvas.sortingOrder = 1;
        //Turns off the override of sorting for the slot
        canvas.overrideSorting = false;

        if (fromSlot != toSlot)
        {
            Debug.Log($"[DEBUG] Move Items - fromSlot: {fromSlot}, toSlot: {toSlot}, start: {drag.parent.name}, release: {parent.name}");
            //if (drag.parent.name.Equals(parent.name) && parent.name.StartsWith("Inventory")) characterManager.CharacterInventory.Swap(fromSlot, toSlot);
        }
    }
}
