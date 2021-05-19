using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GroundItemManager : Singleton<GroundItemManager>
{
    private ItemManager itemManager => ItemManager.Instance();
    
    private List<GroundItemData> groundItems = new List<GroundItemData>();

    public GroundItemData ForGameObject(GameObject gObject)
    {
        return groundItems.FirstOrDefault(groundItem => groundItem.gObject.Equals(gObject));
    }

    private void Awake()
    {
        Test();
    }

    void Update()
    {
        try
        {
            List<GroundItemData> toRemove = new List<GroundItemData>();
            foreach (GroundItemData groundItem in groundItems)
            {
                if (groundItem == null) continue;

                if (groundItem.currentTime > 0)
                {
                    if(false) Debug.Log(groundItem.state + ", time: " + groundItem.currentTime);
                    groundItem.currentTime -= Time.deltaTime;
                }

                if (!(groundItem.currentTime <= 0)) continue;
                
                switch (groundItem.state)
                {
                    case State.HIDDEN:
                        groundItem.state = State.PUBLIC;
                        groundItem.currentTime = groundItem.defaultTime;
                        groundItem.gObject.SetActive(true);
                        break;
                    case State.PRIVATE:
                        groundItem.currentTime = groundItem.defaultTime;
                        groundItem.state = State.PUBLIC;
                        break;
                    case State.PUBLIC:
                        //Don't do anything if the item is a public respawnable item
                        if (!groundItem.respawns)
                        {
                            Remove(groundItem.gObject);
                            toRemove.Add(groundItem);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (GroundItemData groundItem in toRemove) groundItems.Remove(groundItem);
        }
        catch (Exception e)
        {
            Debug.LogError("Error in ground item manager: " + e.Message);
        }
    }
    
    public void Add(Vector3 position, bool respawn, State state, int itemId, int itemAmount = 1)
    {
        var itemToSpawn = itemManager.itemDefinition[itemId];
        var groundItem = itemToSpawn.groundItem;
        
        GameObject gObject = Instantiate(groundItem.prefab, position, Quaternion.identity);
        Setup(groundItem, gObject);
        
        GroundItemData groundItemData = new GroundItemData(new ItemData(itemToSpawn, itemAmount), gObject, state, respawn);
        //new GroundItemData(new ItemData(itemToSpawn, itemAmount), gObject, State.PUBLIC, true);
        groundItems.Add(groundItemData);
    }

    public void Remove(GameObject gObject)
    {
        Remove(ForGameObject(gObject));
    }

    public void Remove(GroundItemData groundItemData)
    {
        if (groundItemData == null)
        {
            Debug.LogError("Ground item is null");
            return;
        }
        
        if (groundItemData.state == State.PUBLIC && groundItemData.respawns)
        {
            groundItemData.state = State.HIDDEN;
            groundItemData.currentTime = groundItemData.defaultTime;
            groundItemData.gObject.SetActive(false);
        }
        else
        {
            groundItems.Remove(groundItemData);
            Destroy(groundItemData.gObject);
        }
    }

    public void Test()
    {
        Add(new Vector3(-3.662f, 2f, -0.5468534f), true, State.PUBLIC, 1);
    }

    void Setup(GroundItem groundItem, GameObject gObject)
    {
        //Rotates the object
        if(groundItem.updateRotation)
            gObject.transform.Rotate(90, 0, 0);
        //Updates the scale
        if(groundItem.updateScale)
            gObject.transform.localScale = new Vector3(50, 50, 50);
        //Sets the gameobject layer to 'GroundItem'
        gObject.layer = 3;

        //Handles the rigidbody settings
        Rigidbody rigidbody = gObject.AddComponent<Rigidbody>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        //Handles the box collider settings
        if (groundItem.needsCollider)
        {
            BoxCollider collider = gObject.AddComponent<BoxCollider>();
            if (groundItem.updateScale)
                collider.size = new Vector3(0.02f, 0.02f, 0.0001f);
            else
                collider.size = new Vector3(1, groundItem.sizeY, 1);
        }

        //Handles the interaction
        GroundItemInteraction interaction = gObject.AddComponent<GroundItemInteraction>();
        
        //Removes the item information component
        Destroy(gObject.GetComponent<ItemInformation>());
        
        //Removes the female part of the item
        if (groundItem.item.fantasyId != -1)
        {
            GameObject female = gObject.transform.GetChild(0).gameObject;
            Destroy(female);
        }
    }
}
