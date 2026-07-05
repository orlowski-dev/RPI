public abstract record NavigationIntent;

public record ExitGame() : NavigationIntent;

public record CharacterCreator() : NavigationIntent;

public record TeleportToDungeon() : NavigationIntent;
