using UnityEngine;

public class ObjectInteractionManager : MonoBehaviour
{
    [SerializeField] private ObjectData objectData;

    public ObjectData GetObjectData()
    {
        return objectData;
    }

    public virtual void OnInteraction(CharacterManager characterManager)
    {
        Debug.Log("Base interaction debug...");
    }
}
