public class EnemyAnimations
{
    public enum Anim
    {
        Idle,
        Atack,
    }

    private Dictionary<EnemyType, string> _animLib { get; } =
        new()
        {
            [EnemyType.Mutant] = "mutant_animlib",
            [EnemyType.Jolleen] = "jolleen_animlib",
            [EnemyType.Maw] = "maw_animlib",
            [EnemyType.SkeletonZombie] = "zombie_animlib",
            [EnemyType.Warrok] = "warrok_animlib",
        };

    private Dictionary<EnemyType, Dictionary<Anim, string>> _anims { get; } =
        new()
        {
            [EnemyType.Mutant] = new()
            {
                [Anim.Idle] = "anim_mutant_idle",
                [Anim.Atack] = "anim_jump_attack",
            },
            [EnemyType.Jolleen] = new()
            {
                [Anim.Idle] = "anim_orc_idle",
                [Anim.Atack] = "anim_surprise_attack_uppercut",
            },
            [EnemyType.Maw] = new()
            {
                [Anim.Idle] = "anim_mutant_idle",
                [Anim.Atack] = "anim_standing_melee_attack_downward",
            },
            [EnemyType.SkeletonZombie] = new()
            {
                [Anim.Idle] = "anim_zombie_idle",
                [Anim.Atack] = "standing_melee_attack_horizontal",
            },
            [EnemyType.Warrok] = new()
            {
                [Anim.Idle] = "anim_zombie_idle",
                [Anim.Atack] = "standing_melee_attack_horizontal",
            },
        };

    public string GetAnimation(EnemyType enemyType, Anim animation)
    {
        return $"{_animLib[enemyType]}/{_anims[enemyType][animation]}";
    }
}
