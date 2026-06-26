public abstract record NavigationIntent;

public record ExitGame() : NavigationIntent;

public record NewGame() : NavigationIntent;
