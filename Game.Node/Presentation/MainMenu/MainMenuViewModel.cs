// co pokazać, gdzie przejść
public record MainMenuViewModel(NavigationIntent Navigation, Error? Error)
    : ARViewModel(Navigation, Error);
