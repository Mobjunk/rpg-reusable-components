using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUIManger<T> : Singleton<T> where T : MonoBehaviour
{
    public List<AbstractItemContainer> containers = new List<AbstractItemContainer>();
    
    private AbstractItemInventory containmentContainer;
    public AbstractItemInventory ContainmentContainer
    {
        get => containmentContainer;
        set => containmentContainer = value;
    }

    [SerializeField] private GameObject containmentPrefab;

    public GameObject ContainmentPrefab
    {
        get => containmentPrefab;
        set => containmentPrefab = value;
    }
    
    [SerializeField] private GameObject inventoryUI;

    public GameObject InventoryUI
    {
        get => inventoryUI;
        set => inventoryUI = value;
    }
    
    [SerializeField] private Transform inventoryContainer;

    public Transform InventoryContainer
    {
        get => inventoryContainer;
        set => inventoryContainer = value;
    }
    
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
        if (ContainmentPrefab == null)
        {
            Debug.LogError("ContainmentPrefab = null");
            return;
        }
        if (InventoryContainer == null)
        {
            Debug.LogError("InventoryContainer = null");
            return;
        }
        Debug.Log("ContainmentContainer.items.Length: " + ContainmentContainer.items.Length);
        //Handles setting up the container
        for (int index = 0; index < ContainmentContainer.items.Length; index++)
        {
            GameObject containment = Instantiate(ContainmentPrefab, InventoryContainer, true);
            containment.name = $"{index}";
            containment.transform.localScale = new Vector3(1, 1, 1);

            AbstractItemContainer container = containment.GetComponent<AbstractItemContainer>();
            
            container.SetContainment(ContainmentContainer.items[index]);
            containers.Add(container);
        }
    }
}
