public partial class MainMenuPresenter
{
    private readonly LoadMetaUseCase _loadMetaUC;
    private readonly LoadGameUseCase _loadGameUC;

    public MainMenuPresenter(LoadMetaUseCase loadMetaUC, LoadGameUseCase loadGameUC)
    {
        _loadMetaUC = loadMetaUC;
        _loadGameUC = loadGameUC;
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
}
