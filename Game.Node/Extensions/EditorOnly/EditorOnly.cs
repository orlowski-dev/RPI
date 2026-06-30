using Godot;

public partial class EditorOnly : Node3D
{
    private Area3D _eventArea = null!;
    private Action? _onClicked;

    public Action? OnClicked
    {
        get => _onClicked;
        set
        {
            _onClicked = value;
            if (_eventArea != null)
                _eventArea.Visible = _onClicked is not null;
        }
    }

    public override void _Ready()
    {
        // if (!Engine.IsEditorHint())
        //     GetNode<Node3D>("%cube").QueueFree();

        _eventArea = GetNode<Area3D>("%EventArea");
        _eventArea.InputRayPickable = true;
        _eventArea.InputEvent += OnAreaInputEvent;
        _eventArea.Visible = _onClicked is not null;

        // GD.Print(
        //     $"EventArea ready, InputRayPickable={_eventArea.InputRayPickable}, monitoring={_eventArea.Monitoring}"
        // );
    }

    private void OnAreaInputEvent(
        Node camera,
        InputEvent @event,
        Vector3 position,
        Vector3 normal,
        long shapeIdx
    )
    {
        // GD.Print($"InputEvent fired: {@event.GetType().Name}");

        if (@event is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
        {
            _onClicked?.Invoke();
        }
    }
}
