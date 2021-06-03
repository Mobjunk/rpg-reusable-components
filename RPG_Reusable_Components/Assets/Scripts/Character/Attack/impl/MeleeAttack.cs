public class MeleeAttack : DefaultAttack
{
    public MeleeAttack(CharacterManager characterManager) : base(characterManager) { }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.ATTACKING_MELEE;
    }
}