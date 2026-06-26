using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class MainScript : Control
{
    private MainMenuPresenter _presenter = null!;

    [Export]
    Button ExitButton = null!;

    [Export]
    Button NewGameButton = null!;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetService<MainMenuPresenter>()!;

        if (_presenter is null)
        {
            DebugExtension.Fatal(this, "Presenter is null.");
        }

        ExitButton.Pressed += OnExit;
        NewGameButton.Pressed += OnNewGame;

        GD.Print("Main menu loaded.");
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
