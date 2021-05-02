using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public virtual void OnInteraction(CharacterManager characterManager)
    {
        Debug.Log("Base interaction debug...");
    }
}
