using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class MainScript : Control
{
    private MainMenuPresenter _presenter = null!;

    private enum Buttons
    {
        Exit,
        NewGame,
        Contunue,
    }

    private Dictionary<Buttons, Button> _buttons = new();

    private MetaSnapshot? _lastSession;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetService<MainMenuPresenter>()!;

        if (_presenter is null)
        {
            DebugExtension.Fatal(this, "Presenter is null.");
        }

        _buttons[Buttons.NewGame] = GetNode<Button>("%NewGame");
        _buttons[Buttons.Contunue] = GetNode<Button>("%ContinueGame");
        _buttons[Buttons.Exit] = GetNode<Button>("%ExitGame");

        _buttons[Buttons.Exit].Pressed += OnExit;
        _buttons[Buttons.NewGame].Pressed += OnNewGame;
        _buttons[Buttons.Contunue].Pressed += OnContinueGame;

        GD.Print("Main menu loaded.");

        _lastSession = _presenter.OnViewLoad().MetaSnapshot;
    }

    public override void _ExitTree()
    {
        _buttons[Buttons.Exit].Pressed -= OnExit;
        _buttons[Buttons.NewGame].Pressed -= OnNewGame;
        _buttons[Buttons.Contunue].Pressed -= OnContinueGame;
    }

    private void OnExit()
    {
        var vm = _presenter.Exit();
        Navigate(vm.Navigation);
    }

    private void OnNewGame()
    {
        var vm = _presenter.NewGame();
        Navigate(vm.Navigation);
    }

    private void OnContinueGame()
    {
        if (_lastSession is null)
            return;
        _presenter.OnContinueGame(_lastSession.LastSessionId);
        GetTree().CallDeferred("change_scene_to_file", ScenePaths.City);
    }

    private void Navigate(NavigationIntent navigation)
    {
        switch (navigation)
        {
            case CharacterCreator:
                GetTree().CallDeferred("change_scene_to_file", ScenePaths.CharacterCreator);
                break;
            case ExitGame:
                GetTree().Quit();
                break;
        }
    }
}
