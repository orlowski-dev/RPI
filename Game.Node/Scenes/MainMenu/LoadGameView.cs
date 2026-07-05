using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class LoadGameView : Control
{
	private MainMenuPresenter _presenter = null!;
	private Container _saveContainer = null!;
	private IReadOnlyList<GameSnapshot>? _snapshots;
	private Button _backButton = null!;

	public override void _Ready()
	{
		_presenter = ServiceProviderHolder.Provider.GetRequiredService<MainMenuPresenter>();
		_saveContainer = GetNode<Container>("%SaveContainer");
		_backButton = GetNode<Button>("%BackButton");
		_backButton.Pressed += () =>
		{
			Visible = false;
		};

		_snapshots = _presenter.OnLoadGameViewLoad().Snapshots ?? new List<GameSnapshot>();
	}

	public void UpdateUI()
	{
		ClearContainer(_saveContainer);
		SpawnSaveList();
	}

	private void ClearContainer(Container container)
	{
		foreach (var child in container.GetChildren())
		{
			child.QueueFree();
		}
	}

	private void SpawnSaveList()
	{
		if (_snapshots is null || _snapshots.Count == 0)
			return;

		var i = 1;
		foreach (var save in _snapshots)
		{
			var entry = GD.Load<PackedScene>(ScenePaths.SaveEntry).Instantiate<Container>();
			_saveContainer.AddChild(entry);
			entry.GetNode<Label>("%Title").Text = $"Zapis {i}";
			entry.GetNode<Label>("%Description").Text = $" {save.CreatedAt.ToLocalTime()}";
			entry.GetNode<Button>("%LoadButton").Pressed += () => OnLoadSave(save.Id);
			i += 1;
		}
	}

	private void OnLoadSave(Guid sessionId)
	{
		_presenter.OnContinueGame(sessionId);
		GetTree().CallDeferred("change_scene_to_file", ScenePaths.City);
	}

	public void SetVisible()
	{
		UpdateUI();
		Visible = true;
	}

	public void SetInvisible()
	{
		Visible = false;
	}
}
