public class CharacterClassesDB
{
    public Dictionary<string, CharacterClass> CharacterClasses { get; set; }

    public CharacterClassesDB()
    {
        CharacterClasses = new()
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
                    MaxhpBonus: 20, // Zmienione na MaxHpBonus z HpBonus
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
                    MaxhpBonus: 12, // Zmienione na MaxHpBonus z HpBonus
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
                    MaxhpBonus: 15, // Zmienione na MaxHpBonus z HpBonus
                    attackBonus: 4,
                    defenseBonus: 2,
                    classIconName: "warrior-icon.png",
                    previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/archer.jpeg",
                    nodeName: "res://Scenes/Characters/Players/Archer.tscn"
                )
            },
        };
    }
}
