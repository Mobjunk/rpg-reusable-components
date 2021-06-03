using UnityEngine;

public delegate void CharacterInputAction();
public delegate void CharacterInputActionMove(Vector2 characterInputs, float characterRotation, bool characterRunning);

public interface ICharacterInput {
    event CharacterInputAction OnCharacterAttack;
    event CharacterInputActionMove OnCharacterMovement;
    event CharacterInputAction OnCharacterInteraction;
}
