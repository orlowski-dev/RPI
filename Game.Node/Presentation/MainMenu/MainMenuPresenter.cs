public partial class MainMenuPresenter
{
    public MainMenuViewModel Exit()
    {
        return new(new ExitGame(), null);
    }
}
