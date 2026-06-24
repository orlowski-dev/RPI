using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class MainMenuScene : Control
{
	private MainMenuPresenter _presenter = null!;

	// limitacje unique id
	[Export]
	VBoxContainer ButtonsCotainer = null!;

	string Box => ButtonsCotainer?.GetPath() ?? "cannot get path";

	string ConcatPath(string elementID) => string.Join('/', [Box, elementID]);

	public override void _Ready()
	{
		_presenter = ServiceProviderHolder.Provider.GetService<MainMenuPresenter>()!;

		if (_presenter is null)
		{
			DebugExtension.Fatal(this, "Presenter is null.");
		}

		GetNode<TextureButton>(ConcatPath("ExitMainMenuGameButton")).Pressed += () => OnExit();

		GD.Print("Main menu loaded.");
	}

	public void OnNewGame()
	{
		var vm = _presenter.NewGame();
		Navigate(vm.Navigation);
	}

	public void OnExit()
	{
		var vm = _presenter.Exit();
		Navigate(vm.Navigation);
	}

	private void Navigate(NavigationIntent navigation)
	{
		switch (navigation)
		{
			case ExitApplication:
				GetTree().Quit();
				break;
		}
	}
}
