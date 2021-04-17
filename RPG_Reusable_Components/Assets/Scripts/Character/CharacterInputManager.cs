using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    public delegate void CharacterAction();

    public CharacterAction OnCharacterMovement = delegate {  };
    public CharacterAction OnCharacterAttack = delegate {  };

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        OnCharacterMovement();
    }
}
