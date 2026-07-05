public partial class MainMenuPresenter
{
    private readonly LoadMetaUseCase _loadMetaUC;
    private readonly LoadGameUseCase _loadGameUC;
    private readonly ListSavesUseCase _listSavesUC;

    public MainMenuPresenter(
        LoadMetaUseCase loadMetaUC,
        LoadGameUseCase loadGameUC,
        ListSavesUseCase listSavesUseCase
    )
    {
        _loadMetaUC = loadMetaUC;
        _loadGameUC = loadGameUC;
        _listSavesUC = listSavesUseCase;
    }

    public MainMenuViewModel Exit()
    {
        return new(new ExitGame(), null);
    }

    public MainMenuViewModel NewGame()
    {
        return new(new CharacterCreator(), null);
    }

    public MainMenuOnLoadViewModel OnViewLoad()
    {
        var res = _loadMetaUC.Execute(new());

        return new(MetaSnapshot: res.Value.MetaSnapshot);
    }

    public void OnContinueGame(Guid sessionId)
    {
        _loadGameUC.Execute(new(SnapshotId: sessionId));
    }

    public MainMenuSaveListViewModel OnLoadGameViewLoad()
    {
        var res = _listSavesUC.Execute(new());
        return new(Snapshots: res.Value.GameSnapshots);
    }
}
