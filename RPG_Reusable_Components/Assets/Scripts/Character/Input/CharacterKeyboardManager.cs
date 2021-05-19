using System;
using UnityEngine;
public class CharacterKeyboardManager : MonoBehaviour, ICharacterInput
 {
     public event CharacterInputAction OnCharacterAttack = delegate {  };
     public event CharacterInputActionMove OnCharacterMovement = delegate {  };
     public event CharacterInputAction OnCharacterInteraction = delegate {  };
     
     private void Update()
     {
         if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
         if (Input.GetKeyDown(KeyCode.F)) OnCharacterInteraction();
         
         Vector2 characterInputs = Vector2.zero;
         float characterRotation = 0;
         bool characterRunning = Input.GetKey(KeyCode.LeftShift);
         
         bool pressingForwards = Input.GetKey(KeyCode.W);
         bool pressingBackwards = Input.GetKey(KeyCode.S);
         bool pressingRotateLeft = Input.GetKey(KeyCode.A);
         bool pressingRotateRight = Input.GetKey(KeyCode.D);
        
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