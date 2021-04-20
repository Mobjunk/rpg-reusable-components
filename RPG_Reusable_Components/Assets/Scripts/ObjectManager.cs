using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField] private List<RespawnableObjects> respawnableObjects = new List<RespawnableObjects>();

    private void Update()
    {
        //Checks if there are any objects that need respawning
        if (respawnableObjects.Count > 0)
        {
            //Creates a list to adjust the normal list later
            //As we are not allowed to remove from a list while looping though it
            List<RespawnableObjects> toRemove = new List<RespawnableObjects>();
            foreach(RespawnableObjects respawnableObject in respawnableObjects)
            {
                //Checks if the current time is higher then the respawn time
                if (Time.time > respawnableObject.respawnTime)
                {
                    //Handles removing the spawned object
                    DestroyImmediate(respawnableObject.newObject);
                    //Sets the old object active again
                    respawnableObject.oldObject.SetActive(true);
                    
                    toRemove.Add(respawnableObject);
                }
            }

            //Loops though all objects that need to be removed
            foreach (RespawnableObjects respawnableObject in toRemove) respawnableObjects.Remove(respawnableObject);
        }
    }

    /// <summary>
    /// Handles replacing a current object withanother object
    /// This is for objects that dont need to respawn
    /// </summary>
    /// <param name="oldObject"></param>
    /// <param name="newObject"></param>
    public void ReplaceGameObject(GameObject oldObject, GameObject newObject)
    {
        ReplaceGameObject(oldObject, newObject, 0);
    }

    /// <summary>
    /// Handles replacing a object with a respawn in mind
    /// </summary>
    /// <param name="oldObject">Object that will be hidden</param>
    /// <param name="newObject">Object that will replace the oldObject</param>
    /// <param name="respawnTime">How long the respawn time is</param>
    public void ReplaceGameObject(GameObject oldObject, GameObject newObject, int respawnTime)
    {
        GameObject gObject = (GameObject)Object.Instantiate(newObject);
        gObject.transform.position = oldObject.transform.position;
        gObject.transform.rotation = oldObject.transform.rotation;
        gObject.transform.parent = oldObject.transform.parent;

        if(respawnTime < 1) DestroyImmediate(oldObject);
        else
        {
            oldObject.SetActive(false);
            respawnableObjects.Add(new RespawnableObjects(Time.time + respawnTime, oldObject, gObject));
        }
    }

}

[Serializable]
public class RespawnableObjects
{
    public float respawnTime;
    public GameObject oldObject;
    public GameObject newObject;

    public RespawnableObjects(float respawnTime, GameObject oldObject, GameObject newObject)
    {
        this.respawnTime = respawnTime;
        this.oldObject = oldObject;
        this.newObject = newObject;
    }
}
