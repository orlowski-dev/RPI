using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CitySceneScript : Node
{
	private CityPresenter _presenter = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Player _player =>
		_gameSessionProvider.Current?.Player ?? throw new Exception("Player instance is null.");
	private EditorOnly _spawnPoint = null!;

	public override void _Ready()
	{
		_presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		_spawnPoint = GetNode<EditorOnly>("%PlayerSpawnPoint");
		SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		var playerScene = GD.Load<PackedScene>(_player.NodePath);
		var playerIns = playerScene.Instantiate<Node2D>();
		playerIns.Position = _spawnPoint.Position;
		playerIns.Scale = new Vector2(.2f, .2f);
		AddChild(playerIns);
	}
}
