// co pokazać, gdzie przejść
public record MainMenuViewModel(NavigationIntent Navigation, Error? Error)
    : ARViewModel(Navigation, Error);

public record MainMenuOnLoadViewModel(MetaSnapshot? MetaSnapshot);

public record MainMenuSaveListViewModel(IReadOnlyList<GameSnapshot> Snapshots);
