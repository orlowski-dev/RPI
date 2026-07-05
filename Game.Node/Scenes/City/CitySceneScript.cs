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
    private Control? _shopView;
    private ItemShopView? _shopViewScript;
    private EventBus _eventBus = null!;
    private bool _canEnterShop = false;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
        _eventBus = ServiceProviderHolder.Provider.GetRequiredService<EventBus>();
        _gameSessionProvider =
            ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

        _eventBus.OnPlayerEnteredSellerArea += OnSellerAreaEntered;
        _eventBus.OnPlayerExitedSellerArea += OnSellerAreaExited;

        _playerSpawnPoint = GetNode<Node3D>("PlayerSpawnPoint");

        AddChild(
            new PlayerSpawner().GetNode(
                _player.NodePath,
                position: new Vector3(_spawnPos.X, 0, _spawnPos.Z)
            )
        );
        AddChild(new FollowCameraSpawner().GetNode());

        _presenter.OnViewLoad();
        SpawnItemShopView();
    }

    public override void _ExitTree()
    {
        _eventBus.OnPlayerEnteredSellerArea -= OnSellerAreaEntered;
        _eventBus.OnPlayerExitedSellerArea -= OnSellerAreaExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("interaction") && _canEnterShop)
        {
            _shopViewScript?.SetVisible();
        }
    }

    private void SpawnItemShopView()
    {
        var shopView = GD.Load<PackedScene>(ScenePaths.ItemShopView).Instantiate<Control>();
        GetNode<CanvasLayer>("CanvasLayer").AddChild(shopView);
        _shopViewScript = (shopView as ItemShopView)!;
        _shopViewScript.OnCloseAction = () =>
        {
            _shopViewScript.SetInvisible();
        };
        _shopViewScript.SetInvisible();
    }

    private void OnSellerAreaEntered()
    {
        _canEnterShop = true;
    }

    private void OnSellerAreaExited()
    {
        _canEnterShop = false;
        _shopViewScript?.SetInvisible();
    }
}
