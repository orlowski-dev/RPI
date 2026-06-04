/// <summary>
/// Centralny magazyn danych i konfiguracji dla modułu Game.Core
/// </summary>
public static partial class CoreService
{
    /// <summary>Domyślna konfiguracja generatora lochu.</summary>
    public static readonly DungGeneratorConfig DungeonGeneratorConfig = new(
        minRoomSize: 20,
        maxRoomSize: 40,
        totalRooms: 5,
        roomOffset: 8,
        doorSize: 1
    );

    /// <summary>
    /// Mapa typów kafelków lochu na listę możliwych tekstur (punktów w atlasie).
    /// Używana do losowania wariantów kafelków podczas generowania.
    /// </summary>
    public static readonly Dictionary<DungTileType, List<Point>> DungeonTiles = new()
    {
        {
            DungTileType.WallTop,
            new List<Point>() { new(2, 0), new(3, 0), new(4, 0) }
        },
        {
            DungTileType.WallBottom,
            new List<Point>() { new(1, 4), new(2, 4), new(3, 4), new(4, 4) }
        },
        {
            DungTileType.WallBottomLeft,
            new List<Point> { new(0, 4) }
        },
        {
            DungTileType.WallBottomRight,
            new List<Point> { new(5, 4) }
        },
        {
            DungTileType.WallLeft,
            new List<Point>() { new(0, 1), new(0, 2), new(0, 3) }
        },
        {
            DungTileType.WallTopLeft,
            new List<Point> { new(0, 0) }
        },
        {
            DungTileType.WallRight,
            new List<Point>() { new(5, 1), new(5, 2), new(5, 3) }
        },
        {
            DungTileType.WallTopRight,
            new List<Point> { new(5, 0) }
        },
        {
            DungTileType.Floor,
            new List<Point>()
            {
                new(1, 1),
                new(2, 1),
                new(3, 1),
                new(4, 1),
                new(1, 2),
                new(2, 2),
                new(3, 2),
                new(4, 2),
                new(1, 3),
                new(2, 3),
                new(3, 3),
                new(4, 3),
            }
        },
        {
            DungTileType.Door,
            new List<Point>() { new(9, 3) }
        },
    };

    /// <summary>
    /// Losuje jeden wariant tekstury dla danego typu kafelka.
    /// </summary>
    /// <param name="type">Typ kafelka.</param>
    /// <returns>Współrzędne tekstury w atlasie.</returns>
    public static Point GetRandomDungTile(DungTileType type)
    {
        var tiles = DungeonTiles[type];
        return tiles[new Random().Next(0, tiles.Count)];
    }

    public static Dictionary<string, CharacterClass> CharacterClasses = new()
    {
        {
            "warrior",
            new(
                name: "Wojownik",
                hpBase: 140,
                attackBase: 12,
                defenseBase: 10,
                critBase: 5,
                luckBase: 2,
                maxHpBonus: 20, // Zmienione na maxHpBonus z HpBonus
                attackBonus: 3,
                defenseBonus: 3,
                classIconName: "warrior-icon.png",
                previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/warrior.jpeg",
                nodeName: "res://Scenes/Characters/Players/Knight.tscn"
            )
        },
        {
            "mage",
            new(
                name: "Mag",
                hpBase: 80,
                attackBase: 18,
                defenseBase: 4,
                critBase: 10,
                luckBase: 4,
                maxHpBonus: 12, // Zmienione na maxHpBonus z HpBonus
                attackBonus: 5,
                defenseBonus: 1,
                classIconName: "mag-icon.png",
                previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/mage.jpeg",
                nodeName: "res://Scenes/Characters/Players/Mage.tscn"
            )
        },
        {
            "archer",
            new(
                name: "Łucznik",
                hpBase: 100,
                attackBase: 14,
                defenseBase: 6,
                critBase: 15,
                luckBase: 6,
                maxHpBonus: 15, // Zmienione na maxHpBonus z HpBonus
                attackBonus: 4,
                defenseBonus: 2,
                classIconName: "warrior-icon.png",
                previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/archer.jpeg",
                nodeName: "res://Scenes/Characters/Players/Archer.tscn"
            )
        },
    };

    public static Dictionary<EnemyType, EnemyCharacter> EnemyTypes = new()
    {
        {
            EnemyType.Zombie,
            new EnemyCharacter(
                name: "Zombie",
                maxHp: 90,
                attack: 10,
                defense: 4,
                critChance: 3,
                enemyType: EnemyType.Zombie,
                level: 1
            )
        },
        {
            EnemyType.Biegacz,
            new EnemyCharacter(
                name: "Biegacz",
                maxHp: 70,
                attack: 15,
                defense: 3,
                critChance: 10,
                enemyType: EnemyType.Biegacz,
                level: 1
            )
        },
        {
            EnemyType.Zboj,
            new EnemyCharacter(
                name: "Zbój",
                maxHp: 110,
                attack: 18,
                defense: 7,
                critChance: 12,
                enemyType: EnemyType.Zboj,
                level: 1
            )
        },
        {
            EnemyType.Brutal,
            new EnemyCharacter(
                name: "Brutal",
                maxHp: 160,
                attack: 22,
                defense: 14,
                critChance: 5,
                enemyType: EnemyType.Brutal,
                level: 1
            )
        },
        {
            EnemyType.Boss,
            new EnemyCharacter(
                name: "Boss",
                maxHp: 300,
                attack: 35,
                defense: 20,
                critChance: 20,
                enemyType: EnemyType.Boss,
                level: 1
            )
        },
    };
}
