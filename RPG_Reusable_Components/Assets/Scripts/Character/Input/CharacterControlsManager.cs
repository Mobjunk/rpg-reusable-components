using UnityEngine;

public class CharacterControlsManager : MonoBehaviour
{
    public KeyCode forwards, backwards, rotateLeft, rotateRight, run, jumping, interaction;
    
    public delegate void KeyCodeChange();
    
    public KeyCodeChange OnKeyCodeChange = delegate {  };

    public void ChangeKeyCode(out KeyCode currentKey, KeyCode keyCode)
    {
        currentKey = keyCode;
        OnKeyCodeChange();
    }
}
