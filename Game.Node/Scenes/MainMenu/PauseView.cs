using Godot;

public partial class PauseView : Control
{
    private enum Buttons
    {
        Continue,
        MainMenu,
        ExitGame,
    }

    private Dictionary<Buttons, Button> _buttons = new();

    public override void _Ready()
    {
        _buttons[Buttons.Continue] = GetNode<Button>("%ContinueButton");
        _buttons[Buttons.MainMenu] = GetNode<Button>("%MainMenuButton");
        _buttons[Buttons.ExitGame] = GetNode<Button>("%ExitGameButton");

        _buttons[Buttons.ExitGame].Pressed += OnExitGame;
        _buttons[Buttons.Continue].Pressed += OnContinue;
        _buttons[Buttons.MainMenu].Pressed += OnMainMenu;
    }

    public override void _ExitTree()
    {
        _buttons[Buttons.ExitGame].Pressed -= OnExitGame;
        _buttons[Buttons.Continue].Pressed -= OnContinue;
        _buttons[Buttons.MainMenu].Pressed -= OnMainMenu;
    }

    private void OnContinue()
    {
        Visible = false;
    }

    private void OnExitGame()
    {
        GetTree().Quit();
    }

    private void OnMainMenu()
    {
        GetTree().CallDeferred("change_scene_to_file", ScenePaths.MainMenu);
    }
}
