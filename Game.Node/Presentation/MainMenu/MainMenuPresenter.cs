public partial class MainMenuPresenter
{
    public MainMenuViewModel NewGame()
    {
        throw new NotImplementedException();
    }

    public MainMenuViewModel Exit()
    {
        return new(new ExitApplication(), null);
    }
}
