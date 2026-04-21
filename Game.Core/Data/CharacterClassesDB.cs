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
                    hpBonus: 20,
                    attackBonus: 3,
                    defenseBonus: 3,
                    classIconName: "warrior-icon.png",
                    previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/warrior.jpeg"
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
                    hpBonus: 12,
                    attackBonus: 5,
                    defenseBonus: 1,
                    classIconName: "mag-icon.png",
                    previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/mage.jpeg"
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
                    hpBonus: 15,
                    attackBonus: 4,
                    defenseBonus: 2,
                    classIconName: "warrior-icon.png",
                    previewSpritePath: "res://Assets/Prototype/Assets/Prototype/CharacterPreview/archer.jpeg"
                )
            },
        };
    }
}
