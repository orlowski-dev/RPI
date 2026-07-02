public interface ICharacterAnimationController
{
    Task PlayAttackAnimation();
    Task PlayDeathAnimation();
    bool DeathAnimPlayed { get; }
}
