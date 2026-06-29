public partial class MainMenuPresenter
{
    public MainMenuViewModel Exit()
    {
        return new(new ExitGame(), null);
    }

    public MainMenuViewModel NewGame()
    {
        return new(new CharacterCreator(), null);
    }
}
