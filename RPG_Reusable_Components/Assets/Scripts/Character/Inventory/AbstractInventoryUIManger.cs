using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUIManger<T> : Singleton<T> where T : MonoBehaviour
{
    private AbstractItemInventory containmentContainer;
    public AbstractItemInventory ContainmentContainer
    {
        get => containmentContainer;
        set => containmentContainer = value;
    }

    [SerializeField] private GameObject containmentPrefab;
    public GameObject ContainmentPrefab => containmentPrefab;
    
    [SerializeField] private GameObject inventoryUI;
    public GameObject InventoryUI => inventoryUI;
    
    [SerializeField] private Transform inventoryContainer;

    public Transform InventoryContainer => inventoryContainer;
    
    public bool isOpened;

    public virtual void Open()
    {
        isOpened = true;
        InventoryUI.SetActive(isOpened);
    }

    public virtual void Close()
    {
        isOpened = false;
        InventoryUI.SetActive(isOpened);
        foreach (Transform child in InventoryContainer) Destroy(child.gameObject);
    }

    public virtual void SetupContainer()
    {
        //Handles setting up the container
        for (int index = 0; index < ContainmentContainer.items.Length; index++)
        {
            GameObject containment = Instantiate(ContainmentPrefab, InventoryContainer, true);
            containment.name = $"{index}";

            containment.GetComponent<AbstractItemContainer>().SetContainment(ContainmentContainer.items[index]);
        }
    }
}
