using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class MainScript : Control
{
    private MainMenuPresenter _presenter = null!;

    [Export]
    Button ExitButton = null!;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetService<MainMenuPresenter>()!;

        if (_presenter is null)
        {
            DebugExtension.Fatal(this, "Presenter is null.");
        }

        ExitButton.Pressed += OnExit;

        GD.Print("Main menu loaded.");
    }

    private void OnExit()
    {
        var vm = _presenter.Exit();
        Navigate(vm.Navigation);
    }

    private void Navigate(NavigationIntent navigation)
    {
        switch (navigation)
        {
            case ExitGame:
                GetTree().Quit();
                break;
        }
    }
}
