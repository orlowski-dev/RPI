public static class PlayerDefinitions
{
    public static Dictionary<PlayerType, ActorDefinition> Values { get; } =
        new()
        {
            [PlayerType.Warrior] = new(new(140, 12, 10, 5, 2)),
            [PlayerType.Mage] = new(new(80, 18, 4, 10, 4)),
            [PlayerType.Archer] = new(new(100, 14, 6, 15, 6)),
        };

    public static Dictionary<PlayerType, string> PreviewImages { get; } =
        new()
        {
            [PlayerType.Warrior] = "res://Assets/UI/character select UI/buttons/knight.png",
            [PlayerType.Mage] = "res://Assets/UI/character select UI/buttons/mage.png",
            [PlayerType.Archer] = "res://Assets/UI/character select UI/buttons/archer.png",
        };

    public static Dictionary<PlayerType, string> TypeDescripions { get; } =
        new()
        {
            [PlayerType.Warrior] =
                "Mistrz walki wręcz, wyposażony w ciężki pancerz i potężną broń. Wysoka wytrzymałość oraz obrona pozwalają mu wytrzymać nawet najtrudniejsze starcia.",

            [PlayerType.Mage] =
                "Włada potężną magią zdolną niszczyć wielu wrogów jednocześnie. Choć dysponuje niewielką obroną, nadrabia ogromną siłą zaklęć i wszechstronnością.",
            [PlayerType.Archer] =
                "Zwinny łowca eliminujący przeciwników z bezpiecznej odległości. Polega na wysokiej szansie trafienia krytycznego i precyzyjnych atakach.",
        };

    public static Dictionary<PlayerType, string> TypePlural { get; } =
        new()
        {
            [PlayerType.Warrior] = "Wojownik",
            [PlayerType.Mage] = "Czarodziej",
            [PlayerType.Archer] = "Łucznik",
        };
}
