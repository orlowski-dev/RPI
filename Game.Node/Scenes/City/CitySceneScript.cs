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
    private CanvasLayer? _canvasLayer;
    private Control? _shopView;
    private ItemShopView? _shopViewScript;
    private Control? _pauseView;
    private EventBus _eventBus = null!;
    private bool _canEnterShop = false;
    private bool _canSaveGame = false;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
        _eventBus = ServiceProviderHolder.Provider.GetRequiredService<EventBus>();
        _gameSessionProvider =
            ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

        _eventBus.OnPlayerEnteredSellerArea += OnSellerAreaEntered;
        _eventBus.OnPlayerExitedSellerArea += OnSellerAreaExited;
        _eventBus.OnPlayerEnteredCamfire += OnCamfireAreaEntered;
        _eventBus.OnPlayerExitedCamfire += OnCamfireAreaExited;

        _playerSpawnPoint = GetNode<Node3D>("PlayerSpawnPoint");
        _canvasLayer = GetNode<CanvasLayer>("CanvasLayer");

        AddChild(
            new PlayerSpawner().GetNode(
                _player.NodePath,
                position: new Vector3(_spawnPos.X, 0, _spawnPos.Z)
            )
        );
        AddChild(new FollowCameraSpawner().GetNode());

        _presenter.OnViewLoad();
        SpawnItemShopView();
        SpawnPauseView();
    }

    public override void _ExitTree()
    {
        _eventBus.OnPlayerEnteredSellerArea -= OnSellerAreaEntered;
        _eventBus.OnPlayerExitedSellerArea -= OnSellerAreaExited;
        _eventBus.OnPlayerEnteredCamfire -= OnCamfireAreaEntered;
        _eventBus.OnPlayerExitedCamfire -= OnCamfireAreaExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("interaction") && _canEnterShop)
        {
            _shopViewScript?.SetVisible();
        }

        if (Input.IsActionJustPressed("interaction") && _canSaveGame)
        {
            _presenter.OnSaveGame();
        }

        if (Input.IsActionJustPressed("escape"))
        {
            if (_pauseView is null)
                return;
            _pauseView.Visible = !_pauseView.Visible;
        }
    }

    private void SpawnItemShopView()
    {
        var shopView = GD.Load<PackedScene>(ScenePaths.ItemShopView).Instantiate<Control>();
        _canvasLayer?.AddChild(shopView);
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

    private void OnCamfireAreaEntered()
    {
        _canSaveGame = true;
    }

    private void OnCamfireAreaExited()
    {
        _canSaveGame = false;
    }

    private void SpawnPauseView()
    {
        _pauseView = GD.Load<PackedScene>(ScenePaths.PauseView).Instantiate<Control>();
        _canvasLayer?.AddChild(_pauseView);
        _pauseView.Visible = false;
    }
}
