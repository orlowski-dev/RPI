public static class EnemyDefinitions
{
    private const float GlobalHpMult = 1.0f;
    private const float GlobalAttackMult = 1.0f;
    private const float GlobalDefenseMult = 1.0f;
    private const float GlobalRewardMult = 1.0f;

    // Mnożniki wynikające z rangi przeciwnika
    private static readonly Dictionary<EnemyRank, RankScale> RankScales = new()
    {
        [EnemyRank.Normal] = new(Hp: 1.0f, Attack: 1.0f, Defense: 1.0f, Crit: 1.0f, Reward: 1.0f),
        [EnemyRank.Elite] = new(Hp: 1.2f, Attack: 1.2f, Defense: 1.2f, Crit: 1.2f, Reward: 1.2f),
        [EnemyRank.Champion] = new(Hp: 1.8f, Attack: 1.5f, Defense: 1.6f, Crit: 1.6f, Reward: 2.0f),
        [EnemyRank.Boss] = new(Hp: 2.0f, Attack: 1.75f, Defense: 2.0f, Crit: 2.0f, Reward: 2.5f),
    };

    private sealed record RankScale(
        float Hp,
        float Attack,
        float Defense,
        float Crit,
        float Reward
    );

    private static int Scale(float baseValue, float rankMult, float globalMult = 1f) =>
        Math.Max(0, (int)MathF.Round(baseValue * rankMult * globalMult));

    private static EnemyDefinition Define(
        string name,
        EnemyType type,
        EnemySubType subType,
        EnemyRank rank,
        int baseHp,
        int baseAttack,
        int baseDefense,
        int baseCrit,
        int luck,
        int baseExp,
        int baseGold,
        string nodePath
    )
    {
        var s = RankScales[rank];
        return new(
            Enemy: new(
                name: name,
                expReward: Scale(baseExp, s.Reward, GlobalRewardMult),
                goldReward: Scale(baseGold, s.Reward, GlobalRewardMult),
                stats: new(
                    maxHp: Scale(baseHp, s.Hp, GlobalHpMult),
                    attack: Scale(baseAttack, s.Attack, GlobalAttackMult),
                    defense: Scale(baseDefense, s.Defense, GlobalDefenseMult),
                    criticalChance: Scale(baseCrit, s.Crit),
                    luck: luck
                ),
                rank: rank,
                type: type,
                subType: subType
            ),
            NodePath: nodePath
        );
    }

    private const string JolleenScn = "res://Assets/Models/Characters/Jolleen/jolleen.scn";
    private const string WarrokScn = "res://Assets/Models/Characters/Warrok/warrok_w_kurniawan.scn";
    private const string SkeletonScn =
        "res://Assets/Models/Characters/SkeletonZombie/skeletonzombie_t_avelange.scn";
    private const string MutantScn = "res://Assets/Models/Characters/Mutant/mutant.scn";
    private const string MawScn = "res://Assets/Models/Characters/Maw/maw.scn";

    public static Dictionary<EnemySubType, EnemyDefinition> Values { get; } =
        new()
        {
            [EnemySubType.Jolleen] = Define(
                "Jolleen",
                EnemyType.Jolleen,
                EnemySubType.Jolleen,
                EnemyRank.Normal,
                baseHp: 40,
                baseAttack: 6,
                baseDefense: 1,
                baseCrit: 10,
                luck: 8,
                baseExp: 25,
                baseGold: 8,
                nodePath: JolleenScn
            ),
            [EnemySubType.JolleenElite] = Define(
                "Elitarna Jolleen",
                EnemyType.Jolleen,
                EnemySubType.JolleenElite,
                EnemyRank.Elite,
                baseHp: 40,
                baseAttack: 6,
                baseDefense: 1,
                baseCrit: 10,
                luck: 12,
                baseExp: 25,
                baseGold: 8,
                nodePath: JolleenScn
            ),
            [EnemySubType.JolleenChampion] = Define(
                "Jolleen Mistrzyni",
                EnemyType.Jolleen,
                EnemySubType.JolleenChampion,
                EnemyRank.Champion,
                baseHp: 40,
                baseAttack: 6,
                baseDefense: 1,
                baseCrit: 10,
                luck: 16,
                baseExp: 25,
                baseGold: 8,
                nodePath: JolleenScn
            ),

            [EnemySubType.Warrok] = Define(
                "Warrok",
                EnemyType.Warrok,
                EnemySubType.Warrok,
                EnemyRank.Normal,
                baseHp: 70,
                baseAttack: 9,
                baseDefense: 3,
                baseCrit: 6,
                luck: 3,
                baseExp: 45,
                baseGold: 22,
                nodePath: WarrokScn
            ),
            [EnemySubType.WarrokElite] = Define(
                "Elitarny Warrok",
                EnemyType.Warrok,
                EnemySubType.WarrokElite,
                EnemyRank.Elite,
                baseHp: 70,
                baseAttack: 9,
                baseDefense: 3,
                baseCrit: 6,
                luck: 5,
                baseExp: 45,
                baseGold: 22,
                nodePath: WarrokScn
            ),
            [EnemySubType.WarrokChampion] = Define(
                "Warrok Mistrz",
                EnemyType.Warrok,
                EnemySubType.WarrokChampion,
                EnemyRank.Champion,
                baseHp: 70,
                baseAttack: 9,
                baseDefense: 3,
                baseCrit: 6,
                luck: 6,
                baseExp: 45,
                baseGold: 22,
                nodePath: WarrokScn
            ),

            [EnemySubType.SkeletonZombie] = Define(
                "Szkielet Zombie",
                EnemyType.SkeletonZombie,
                EnemySubType.SkeletonZombie,
                EnemyRank.Normal,
                baseHp: 55,
                baseAttack: 10,
                baseDefense: 2,
                baseCrit: 8,
                luck: 2,
                baseExp: 40,
                baseGold: 18,
                nodePath: SkeletonScn
            ),
            [EnemySubType.SkeletonZombieElite] = Define(
                "Elitarny Szkielet Zombie",
                EnemyType.SkeletonZombie,
                EnemySubType.SkeletonZombieElite,
                EnemyRank.Elite,
                baseHp: 55,
                baseAttack: 10,
                baseDefense: 2,
                baseCrit: 8,
                luck: 4,
                baseExp: 40,
                baseGold: 18,
                nodePath: SkeletonScn
            ),
            [EnemySubType.SkeletonZombieChampion] = Define(
                "Szkielet Zombie Mistrz",
                EnemyType.SkeletonZombie,
                EnemySubType.SkeletonZombieChampion,
                EnemyRank.Champion,
                baseHp: 55,
                baseAttack: 10,
                baseDefense: 2,
                baseCrit: 8,
                luck: 5,
                baseExp: 40,
                baseGold: 18,
                nodePath: SkeletonScn
            ),

            [EnemySubType.Mutant] = Define(
                "Mutant",
                EnemyType.Mutant,
                EnemySubType.Mutant,
                EnemyRank.Normal,
                baseHp: 95,
                baseAttack: 12,
                baseDefense: 4,
                baseCrit: 5,
                luck: 1,
                baseExp: 80,
                baseGold: 55,
                nodePath: MutantScn
            ),
            [EnemySubType.MutantElite] = Define(
                "Elitarny Mutant",
                EnemyType.Mutant,
                EnemySubType.MutantElite,
                EnemyRank.Elite,
                baseHp: 95,
                baseAttack: 12,
                baseDefense: 4,
                baseCrit: 5,
                luck: 2,
                baseExp: 80,
                baseGold: 55,
                nodePath: MutantScn
            ),
            [EnemySubType.MutantChampion] = Define(
                "Mutant Mistrz",
                EnemyType.Mutant,
                EnemySubType.MutantChampion,
                EnemyRank.Champion,
                baseHp: 95,
                baseAttack: 12,
                baseDefense: 4,
                baseCrit: 5,
                luck: 3,
                baseExp: 80,
                baseGold: 55,
                nodePath: MutantScn
            ),

            [EnemySubType.Maw] = Define(
                "Maw",
                EnemyType.Maw,
                EnemySubType.Maw,
                EnemyRank.Normal,
                baseHp: 120,
                baseAttack: 13,
                baseDefense: 5,
                baseCrit: 10,
                luck: 4,
                baseExp: 86,
                baseGold: 180,
                nodePath: MawScn
            ),
            [EnemySubType.MawElite] = Define(
                "Elitarny Maw",
                EnemyType.Maw,
                EnemySubType.MawElite,
                EnemyRank.Elite,
                baseHp: 120,
                baseAttack: 13,
                baseDefense: 5,
                baseCrit: 10,
                luck: 6,
                baseExp: 86,
                baseGold: 180,
                nodePath: MawScn
            ),
            [EnemySubType.MawBoss] = Define(
                "Maw Władca Otchłani",
                EnemyType.Maw,
                EnemySubType.MawBoss,
                EnemyRank.Boss,
                baseHp: 120,
                baseAttack: 13,
                baseDefense: 5,
                baseCrit: 10,
                luck: 8,
                baseExp: 86,
                baseGold: 180,
                nodePath: MawScn
            ),
        };
}
