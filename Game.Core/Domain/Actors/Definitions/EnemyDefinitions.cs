public static class EnemyDefinitions
{
    public static Dictionary<EnemyType, EnemyDefinition> Values { get; } =
        new()
        {
            [EnemyType.Jolleen] = new(
                Enemy: new(
                    name: "Jolleen",
                    expReward: 25,
                    goldReward: 8,
                    stats: new(maxHp: 60, attack: 8, defense: 3, criticalChance: 12, luck: 8),
                    rank: EnemyRank.Normal,
                    type: EnemyType.Jolleen
                ),
                NodePath: "res://Assets/Models/Characters/Jolleen/jolleen.scn"
            ),
            [EnemyType.JolleenElite] = new(
                Enemy: new(
                    name: "Elitarna Jolleen",
                    expReward: 55,
                    goldReward: 18,
                    stats: new(maxHp: 90, attack: 12, defense: 6, criticalChance: 18, luck: 12),
                    rank: EnemyRank.Elite,
                    type: EnemyType.JolleenElite
                ),
                NodePath: "res://Assets/Models/Characters/Jolleen/jolleen.scn"
            ),
            [EnemyType.JolleenChampion] = new(
                Enemy: new(
                    name: "Jolleen Mistrzyni",
                    expReward: 110,
                    goldReward: 35,
                    stats: new(maxHp: 130, attack: 16, defense: 9, criticalChance: 24, luck: 16),
                    rank: EnemyRank.Champion,
                    type: EnemyType.JolleenChampion
                ),
                NodePath: "res://Assets/Models/Characters/Jolleen/jolleen.scn"
            ),

            [EnemyType.Warrok] = new(
                Enemy: new(
                    name: "Warrok",
                    expReward: 90,
                    goldReward: 22,
                    stats: new(maxHp: 120, attack: 13, defense: 7, criticalChance: 6, luck: 3),
                    rank: EnemyRank.Normal,
                    type: EnemyType.Warrok
                ),
                NodePath: "res://Assets/Models/Characters/Warrok/warrok_w_kurniawan.scn"
            ),
            [EnemyType.WarrokElite] = new(
                Enemy: new(
                    name: "Elitarny Warrok",
                    expReward: 180,
                    goldReward: 48,
                    stats: new(maxHp: 175, attack: 18, defense: 11, criticalChance: 10, luck: 5),
                    rank: EnemyRank.Elite,
                    type: EnemyType.WarrokElite
                ),
                NodePath: "res://Assets/Models/Characters/Warrok/warrok_w_kurniawan.scn"
            ),
            [EnemyType.WarrokChampion] = new(
                Enemy: new(
                    name: "Warrok Mistrz",
                    expReward: 300,
                    goldReward: 80,
                    stats: new(maxHp: 240, attack: 22, defense: 15, criticalChance: 13, luck: 6),
                    rank: EnemyRank.Champion,
                    type: EnemyType.WarrokChampion
                ),
                NodePath: "res://Assets/Models/Characters/Warrok/warrok_w_kurniawan.scn"
            ),

            [EnemyType.SkeletonZombie] = new(
                Enemy: new(
                    name: "Szkielet Zombie",
                    expReward: 80,
                    goldReward: 18,
                    stats: new(maxHp: 100, attack: 14, defense: 5, criticalChance: 8, luck: 2),
                    rank: EnemyRank.Normal,
                    type: EnemyType.SkeletonZombie
                ),
                NodePath: "res://Assets/Models/Characters/SkeletonZombie/skeletonzombie_t_avelange.scn"
            ),
            [EnemyType.SkeletonZombieElite] = new(
                Enemy: new(
                    name: "Elitarny Szkielet Zombie",
                    expReward: 160,
                    goldReward: 40,
                    stats: new(maxHp: 150, attack: 19, defense: 8, criticalChance: 13, luck: 4),
                    rank: EnemyRank.Elite,
                    type: EnemyType.SkeletonZombieElite
                ),
                NodePath: "res://Assets/Models/Characters/SkeletonZombie/skeletonzombie_t_avelange.scn"
            ),
            [EnemyType.SkeletonZombieChampion] = new(
                Enemy: new(
                    name: "Szkielet Zombie Mistrz",
                    expReward: 270,
                    goldReward: 70,
                    stats: new(maxHp: 210, attack: 24, defense: 11, criticalChance: 17, luck: 5),
                    rank: EnemyRank.Champion,
                    type: EnemyType.SkeletonZombieChampion
                ),
                NodePath: "res://Assets/Models/Characters/SkeletonZombie/skeletonzombie_t_avelange.scn"
            ),

            [EnemyType.Mutant] = new(
                Enemy: new(
                    name: "Mutant",
                    expReward: 200,
                    goldReward: 55,
                    stats: new(maxHp: 180, attack: 16, defense: 9, criticalChance: 5, luck: 1),
                    rank: EnemyRank.Normal,
                    type: EnemyType.Mutant
                ),
                NodePath: "res://Assets/Models/Characters/Mutant/mutant.scn"
            ),
            [EnemyType.MutantElite] = new(
                Enemy: new(
                    name: "Elitarny Mutant",
                    expReward: 380,
                    goldReward: 105,
                    stats: new(maxHp: 260, attack: 22, defense: 14, criticalChance: 9, luck: 2),
                    rank: EnemyRank.Elite,
                    type: EnemyType.MutantElite
                ),
                NodePath: "res://Assets/Models/Characters/Mutant/mutant.scn"
            ),
            [EnemyType.MutantChampion] = new(
                Enemy: new(
                    name: "Mutant Mistrz",
                    expReward: 600,
                    goldReward: 165,
                    stats: new(maxHp: 350, attack: 28, defense: 18, criticalChance: 12, luck: 3),
                    rank: EnemyRank.Champion,
                    type: EnemyType.MutantChampion
                ),
                NodePath: "res://Assets/Models/Characters/Mutant/mutant.scn"
            ),

            [EnemyType.Maw] = new(
                Enemy: new(
                    name: "Maw",
                    expReward: 600,
                    goldReward: 180,
                    stats: new(maxHp: 280, attack: 20, defense: 12, criticalChance: 10, luck: 4),
                    rank: EnemyRank.Normal,
                    type: EnemyType.Maw
                ),
                NodePath: "res://Assets/Models/Characters/Maw/maw.scn"
            ),
            [EnemyType.MawElite] = new(
                Enemy: new(
                    name: "Elitarny Maw",
                    expReward: 1000,
                    goldReward: 300,
                    stats: new(maxHp: 400, attack: 27, defense: 17, criticalChance: 14, luck: 6),
                    rank: EnemyRank.Elite,
                    type: EnemyType.MawElite
                ),
                NodePath: "res://Assets/Models/Characters/Maw/maw.scn"
            ),
            [EnemyType.MawBoss] = new(
                Enemy: new(
                    name: "Maw Władca Otchłani",
                    expReward: 2000,
                    goldReward: 600,
                    stats: new(maxHp: 600, attack: 35, defense: 22, criticalChance: 18, luck: 8),
                    rank: EnemyRank.Boss,
                    type: EnemyType.MawBoss
                ),
                NodePath: "res://Assets/Models/Characters/Maw/maw.scn"
            ),
        };
}
