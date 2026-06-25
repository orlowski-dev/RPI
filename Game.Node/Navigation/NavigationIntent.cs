public abstract record NavigationIntent;

public record Stay() : NavigationIntent;

public record OpenCharacterCreation() : NavigationIntent;

public record ExitApplication() : NavigationIntent;
