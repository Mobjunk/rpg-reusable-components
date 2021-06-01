using UnityEngine;

public class ObjectInteractionManager : InteractionManager
{
    [SerializeField] private ObjectData objectData;

    public ObjectData GetObjectData()
    {
        return objectData;
    }
}
