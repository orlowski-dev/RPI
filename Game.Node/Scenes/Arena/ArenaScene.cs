using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ArenaScene : Node3D
{
    private enum Sp
    {
        Player,
        Enemy0,
        Enemy1,
        Enemy2,
    }

    private IGameSessionProvider _gameSessionProvider = null!;
    private ArenaPresenter _arenaPresenter = null!;
    private Player _Player => _gameSessionProvider.Current!.Player;
    private CombatSession Combat => _gameSessionProvider.Current!.CombatSession!;
    private GameSession _GameSession => _gameSessionProvider.Current!;
    private SpotLight3D _targetLight = null!;
    private SpotLight3D _participantLight = null!;
    private ArenaViewScript _arenaView = null!;
    private RewardView _rewardView = null!;

    private Dictionary<Sp, EditorOnly> _spawnPoints = new();
    private Dictionary<Actor, Sp> _actorsMap = new();
    private Dictionary<Actor, Node3D> _modelsMap = new();

    private bool _isStepping = false;

    [Export]
    public float StepDelay = 1.0f;

    public override void _Ready()
    {
        _gameSessionProvider =
            ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _arenaPresenter = ServiceProviderHolder.Provider.GetRequiredService<ArenaPresenter>();

        if (_gameSessionProvider.Current is null)
        {
            DebugExtension.Fatal(this, "Current gamesession is null!");
            return;
        }

        var session = _arenaPresenter.OnViewReady(_Player);

        if (_gameSessionProvider.Current.CombatSession is null)
        {
            DebugExtension.Fatal(this, "Combat session in game session is null!");
            return;
        }

        _spawnPoints = new()
        {
            [Sp.Player] = GetNode<EditorOnly>("PlayerSpawn"),
            [Sp.Enemy0] = GetNode<EditorOnly>("EnemySpawn0"),
            [Sp.Enemy1] = GetNode<EditorOnly>("EnemySpawn1"),
            [Sp.Enemy2] = GetNode<EditorOnly>("EnemySpawn2"),
        };

        _targetLight = GetNode<SpotLight3D>("%TargetLight");
        _participantLight = GetNode<SpotLight3D>("%CurrentParticipantLight");

        SpawnModels();
        MoveParticipantLight();
        MoveTargetLight();

        _arenaView = GetNode<ArenaViewScript>("CanvasLayer/ArenaView");
        _arenaView.Init(this);

        _rewardView = GetNode<RewardView>("CanvasLayer/RewardView");
    }

    public void StartCombatSteps()
    {
        if (_isStepping)
            return;

        _isStepping = true;
        _ = RunCombatSteps();
    }

    private async Task RunCombatSteps()
    {
        while (true)
        {
            var hasMore = _arenaPresenter.StepCombat();

            if (!hasMore)
            {
                _arenaView.UpdateUI();
                MoveParticipantLight();
                MoveTargetLight();
                break;
            }

            await PlayAnims();
            MoveParticipantLight();
            MoveTargetLight();
            _arenaView.UpdateUI();
            _arenaView.HidePlayerAction();
        }
        _isStepping = false;
        // sprawdz czy ktoś wziął i umarł
        foreach (var (actor, node) in _modelsMap)
        {
            if (!actor.IsAlive)
            {
                if (node is EnemyScript || node is PlayerController)
                {
                    var s = (node as ICharacterAnimationController)!;
                    await s.PlayDeathAnimation();
                }
            }
        }

        CheckIfCombatIsFinished();
    }

    private async Task PlayAnims()
    {
        var attacker = Combat.LastAttacker;

        Combat.ClearLastAttack();

        if (attacker is null)
        {
            await ToSignal(GetTree().CreateTimer(StepDelay), Godot.Timer.SignalName.Timeout);
            return;
        }

        if (_modelsMap.TryGetValue(attacker, out var script))
        {
            if (script is EnemyScript || script is PlayerController)
            {
                var s = (script as ICharacterAnimationController)!;
                if (Combat?.LastActionResult?.Type == ActionType.Attack)
                {
                    await s.PlayAttackAnimation();
                }
            }
        }
    }

    private void SpawnModels()
    {
        for (var i = 0; i < Combat.Participants.Count; i++)
        {
            var currentPart = Combat.Participants[i];
            // var model = GD.Load<PackedScene>(currentPart.NodePath).Instantiate<Node3D>();
            Node3D model;

            if (currentPart is Enemy)
            {
                model = EnemySpawner.GetNode(currentPart.NodePath);
                AddChild(model);
                var script = (model as EnemyScript);
                if (script is null)
                    return;

                script.Enemy = (Enemy)currentPart;
                script.Label = currentPart.IsAlive ? currentPart.DisplayName : "";
            }
            else
            {
                model = new PlayerSpawner().GetNode(currentPart.NodePath);
                AddChild(model);
                var script = (model as PlayerController);
                if (script is null)
                    return;
                script.ControlEnabled = false;
            }
            var currentSpawn = _spawnPoints[(Sp)i];

            model.Position = new Vector3(currentSpawn.Position.X, 0, currentSpawn.Position.Z);
            _actorsMap[currentPart] = (Sp)i;
            model.RotateY(currentSpawn.Rotation.Y);

            _modelsMap[currentPart] = model;

            currentSpawn.OnClicked += () => OnClickOnEnemy(currentPart);

            model.Name = "Model";
        }
    }

    private void OnClickOnEnemy(Actor actor)
    {
        if (Combat.ActiveParticipant is not Player || !actor.IsAlive || actor is Player)
            return;
        Combat.SetTarget(actor);
        MoveParticipantLight();
        MoveTargetLight();
    }

    private void MoveParticipantLight()
    {
        var targActor = _actorsMap[Combat.ActiveParticipant];
        var targSlotPos = _spawnPoints[targActor].Position;
        _participantLight.Position = new Vector3(
            targSlotPos.X,
            _participantLight.Position.Y,
            targSlotPos.Z
        );
    }

    private void MoveTargetLight()
    {
        if (Combat.Target is null)
        {
            _targetLight.Visible = false;
            return;
        }

        _targetLight.Visible = true;
        var targActor = _actorsMap[Combat.Target];
        var targSlotPos = _spawnPoints[targActor].Position;
        _targetLight.Position = new Vector3(targSlotPos.X, _targetLight.Position.Y, targSlotPos.Z);
    }

    private async void CheckIfCombatIsFinished()
    {
        if (!Combat.IsFinished)
            return;

        GD.Print("Combat finished");
        await ToSignal(GetTree().CreateTimer(3f), Godot.Timer.SignalName.Timeout);

        if (!_Player.IsAlive)
        {
            GetTree().CallDeferred("change_scene_to_file", ScenePaths.Defeat);
            return;
        }

        var vm = _arenaPresenter.OnCombatFinished();
        _arenaView.Visible = false;
        _rewardView.ShowView();
        _GameSession.PendingEncounter?.MarkRewardClaimed();
        _GameSession.PendingEncounter?.End();
    }
}
