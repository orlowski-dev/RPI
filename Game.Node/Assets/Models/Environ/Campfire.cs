using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class Campfire : StaticBody3D
{
    private Area3D _eventArea = null!;
    private Label3D _interactionLabel = null!;
    private EventBus _eventBus = null!;

    public override void _Ready()
    {
        _eventBus = ServiceProviderHolder.Provider.GetRequiredService<EventBus>();
        _eventArea = GetNode<Area3D>("%EventArea");
        _interactionLabel = GetNode<Label3D>("%InteractionLabel");
        _interactionLabel.Text =
            "Wciśnij [" + ActionKey.GetLabel("interaction") + "] aby zapisać grę";
        _interactionLabel.Visible = false;

        _eventArea.BodyEntered += OnBodyEntered;
        _eventArea.BodyExited += OnBodyExited;
    }

    public override void _ExitTree()
    {
        _eventArea.BodyEntered -= OnBodyEntered;
        _eventArea.BodyExited -= OnBodyExited;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is not PlayerController)
            return;
        _interactionLabel.Visible = true;
        _eventBus.PlayerEnteredCamfire();
    }

    private void OnBodyExited(Node3D body)
    {
        _interactionLabel.Visible = false;
        _eventBus.PlayerExitedCamfire();
    }
}
