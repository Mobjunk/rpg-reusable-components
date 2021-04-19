using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    private CharacterControlsManager characterControlsManager;
    
    public delegate void CharacterAction();

    public CharacterAction OnCharacterMovement = delegate {  };
    public CharacterAction OnCharacterAttack = delegate {  };
    public CharacterAction OnCharacterInteraction = delegate {  };

    private void Awake()
    {
        characterControlsManager = GetComponent<CharacterControlsManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        if (Input.GetKeyDown(characterControlsManager.interaction)) OnCharacterInteraction();
        OnCharacterMovement();
    }
}
