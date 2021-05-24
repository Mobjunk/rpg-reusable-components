using System;
using UnityEngine;

public class CurrentDrag : Singleton<CurrentDrag>
{
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
