using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CitySceneScript : Node
{
	private CityPresenter _presenter = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Player _player =>
		_gameSessionProvider.Current?.Player ?? throw new Exception("Player instance is null.");
	private Node3D _playerSpawnPoint = null!;
	private Vector3 _spawnPos => _playerSpawnPoint.Position;

	public override void _Ready()
	{
		_presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		_playerSpawnPoint = GetNode<Node3D>("PlayerSpawnPoint");

		AddChild(
			new PlayerSpawner().GetNode(
				_player.NodePath,
				position: new Vector3(_spawnPos.X, 0, _spawnPos.Z)
			)
		);
		AddChild(new FollowCameraSpawner().GetNode());
	}
}
