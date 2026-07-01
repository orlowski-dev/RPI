public class EnemyAnimations
{
    public enum Anim
    {
        Idle,
        Atack,
        Death,
    }

    private Dictionary<EnemyType, string> _animLib { get; } =
        new()
        {
            [EnemyType.Mutant] = "mixamo",
            [EnemyType.Jolleen] = "mixamo",
            [EnemyType.Maw] = "mixamo",
            [EnemyType.SkeletonZombie] = "mixamo",
            [EnemyType.Warrok] = "mixamo",
        };

    private Dictionary<EnemyType, Dictionary<Anim, string>> _anims { get; } =
        new()
        {
            [EnemyType.Mutant] = new()
            {
                [Anim.Idle] = "mutant_idle",
                [Anim.Atack] = "mutant_swiping",
                [Anim.Death] = "mutant__dying",
            },
            [EnemyType.Jolleen] = new()
            {
                [Anim.Idle] = "orc_idle",
                [Anim.Atack] = "ork_punching",
                [Anim.Death] = "orc_dying_backwards",
            },
            [EnemyType.Maw] = new()
            {
                [Anim.Idle] = "maw_idle",
                [Anim.Atack] = "maw_swiping",
                [Anim.Death] = "maw_dying",
            },
            [EnemyType.SkeletonZombie] = new()
            {
                [Anim.Idle] = "zombie_idle",
                [Anim.Atack] = "zombie_attack",
                [Anim.Death] = "zombie_dying",
            },
            [EnemyType.Warrok] = new()
            {
                [Anim.Idle] = "warror_idle",
                [Anim.Atack] = "warrok_attack",
                [Anim.Death] = "warrok_death",
            },
        };

    public string GetAnimation(EnemyType enemyType, Anim animation)
    {
        return $"{_animLib[enemyType]}/{_anims[enemyType][animation]}";
    }
}
