using System;
using UnityEngine;

public class CharacterControllerManager : MonoBehaviour, ICharacterInput
{
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInputAction OnCharacterInteraction = delegate {  };
     
    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) OnCharacterAttack();
        if (Input.GetButtonDown("Fire2")) OnCharacterInteraction();
         
        Vector2 characterInputs = Vector2.zero;
        float characterRotation = 0;
        bool characterRunning = Input.GetButton("Fire3");

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        bool pressingForwards = yInput >= 0.5f;
        bool pressingBackwards = yInput <= -0.5f;

        bool pressingRotateLeft = xInput <= -0.5f;
        bool pressingRotateRight = xInput >= 0.5f;
        
        //Handles forwards and backwards movement
        if (pressingForwards) characterInputs.y = 1;
        if (pressingBackwards) characterInputs.y = pressingForwards ? 0 : -1;
        
        if (!pressingForwards && !pressingBackwards) characterInputs.y = 0;

        //Handles the left and right rotation of the character
        if (pressingRotateLeft) characterRotation = -1;
        if (pressingRotateRight) characterRotation = pressingRotateLeft ? 0 : 1;
        
        if (!pressingRotateLeft && !pressingRotateRight) characterRotation = 0;
        
        OnCharacterMovement(characterInputs, characterRotation, characterRunning);
    }
}