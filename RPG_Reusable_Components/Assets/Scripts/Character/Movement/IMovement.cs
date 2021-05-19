using UnityEngine;

public interface IMovement
{
    void Move(Vector2 characterInputs, float characterRotation, bool characterRunning);
}
