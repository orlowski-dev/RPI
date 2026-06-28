public static class PlayerDefinitions
{
    public static Dictionary<PlayerType, PlayerDefinition> Values { get; } =
        new()
        {
            [PlayerType.Warrior] = new(
                ActorStats: new(140, 12, 10, 5, 2),
                PreviewImage: "res://Assets/UI/character select UI/buttons/knight.png",
                TypeDescription: "Mistrz walki wręcz, wyposażony w ciężki pancerz i potężną broń. Wysoka wytrzymałość oraz obrona pozwalają mu wytrzymać nawet najtrudniejsze starcia.",
                TypePlural: "Wojownik",
                NodePath: "res://Assets/Models/Characters/Warrior/warrior.scn"
            ),
            [PlayerType.Archer] = new(
                ActorStats: new(100, 14, 6, 15, 6),
                PreviewImage: "res://Assets/UI/character select UI/buttons/archer.png",
                TypeDescription: "Zwinny łowca eliminujący przeciwników z bezpiecznej odległości. Polega na wysokiej szansie trafienia krytycznego i precyzyjnych atakach.",
                TypePlural: "Łucznik",
                NodePath: "res://Assets/Models/Characters/Archer/erika_archer.scn"
            ),
            [PlayerType.Mage] = new(
                ActorStats: new(80, 18, 4, 10, 4),
                PreviewImage: "res://Assets/UI/character select UI/buttons/mage.png",
                TypeDescription: "Włada potężną magią zdolną niszczyć wielu wrogów jednocześnie. Choć dysponuje niewielką obroną, nadrabia ogromną siłą zaklęć i wszechstronnością.",
                TypePlural: "Czarodziej",
                NodePath: "res://Assets/Models/Characters/Mage/mage.scn"
            ),
        };
}
