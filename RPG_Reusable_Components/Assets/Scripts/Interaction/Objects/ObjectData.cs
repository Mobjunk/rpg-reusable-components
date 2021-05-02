using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
[System.Serializable]
public class ObjectData : ScriptableObject
{
    [Header("Interaction")]
    public string interactionMessage;

    [Header("Secondary object")]
    public GameObject secondaryObject;
}