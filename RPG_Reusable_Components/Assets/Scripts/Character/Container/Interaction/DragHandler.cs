using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    //Information for dragging
    public static GameObject itemBeingDragged;

    public GameObject parent;
    public GameObject slot;
      CanvasGroup canvasGroup;
    private Canvas canvas;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform startParent;
    [SerializeField] private bool enableDrag = true;

    public void Start()
    {
        parent = transform.parent.transform.parent.transform.parent.gameObject;
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = transform.parent.GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;

        CurrentDrag.Instance().SetCurrentDrag(this);

        //Sets the item thats being dragged
        itemBeingDragged = gameObject;
        //Sets the start position of the item being dragged
        startPosition = transform.position;
        //Sets the start parent as the game object
        startParent = transform.parent;
        //Sets block raycasting to false
        canvasGroup.blocksRaycasts = false;
        //Turns on the override sorting for the item your dragging
        canvas.overrideSorting = true;
        //Sets the sorting order layer to 2 for the item your dragging so its always on top
        canvas.sortingOrder = 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;
        //Makes the item follow the mouse
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;
        ResetDrag();
    }

    public void ResetDrag()
    {
        if (!enableDrag) return;
     
        //Resets the item being dragged
        itemBeingDragged = null;

        CurrentDrag.Instance().SetCurrentDrag(null);

        //Turns back on the block raycasting
        canvasGroup.blocksRaycasts = true;

        //Turns off the override sorting for the item your dragging
        canvas.overrideSorting = false;
        //Sets the sorting order layer to 1
        canvas.sortingOrder = 1;

        //Sets the position back of the item
        if (transform.parent == startParent)
            transform.position = startPosition;
    }
    
}
