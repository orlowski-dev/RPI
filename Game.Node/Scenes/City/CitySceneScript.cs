using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CitySceneScript : Node
{
	private const string PlayerScenePath = "res://Scenes/Player/player.scn";
	private const string FollowCameraScenePath = "res://Scenes/Camera/follow_camera.tscn";

	private CityPresenter _presenter = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Player _player =>
		_gameSessionProvider.Current?.Player ?? throw new Exception("Player instance is null.");
	private CharacterBody3D _playerNode = null!;

	public override void _Ready()
	{
		_presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		SpawnPlayer();
		SpawnCamera();
	}

	private void SpawnPlayer()
	{
		var playerPacked = GD.Load<PackedScene>(PlayerScenePath);
		var modelScene = GD.Load<PackedScene>(_player.NodePath);

		_playerNode = playerPacked.Instantiate<CharacterBody3D>();
		_playerNode.Name = "Player";

		var model = modelScene.Instantiate<Node3D>();
		model.Name = "Model";
		_playerNode.AddChild(model);

		AddChild(_playerNode);
	}

	private void SpawnCamera()
	{
		var cameraPacked = GD.Load<PackedScene>(FollowCameraScenePath);
		AddChild(cameraPacked.Instantiate());
	}
}
