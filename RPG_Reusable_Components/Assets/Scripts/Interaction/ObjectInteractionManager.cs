using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionManager : MonoBehaviour
{
    [SerializeField] private ObjectData objectData;

    public ObjectData GetObjectData()
    {
        return objectData;
    }
}
