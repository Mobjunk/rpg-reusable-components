using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public List<ContainerSlot> containerSlots = new List<ContainerSlot>();

    private void Awake()
    {
        foreach(Transform child in transform)
            containerSlots.Add(child.GetComponent<ContainerSlot>());

        Debug.Log($"Container {gameObject.name} has been setup with {containerSlots.Count} slots!");
    }
}