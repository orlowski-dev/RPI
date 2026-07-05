using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class Seller : StaticBody3D
{
    private Area3D _eventArea = null!;
    private EventBus _eventBus = null!;
    private Label3D _interactionLabel = null!;

    public override void _Ready()
    {
        _eventBus = ServiceProviderHolder.Provider.GetRequiredService<EventBus>();
        _eventArea = GetNode<Area3D>("EventArea");
        _interactionLabel = GetNode<Label3D>("%InteractionLabel");
        _interactionLabel.Text =
            "[" + ActionKey.GetLabel("interaction") + "] aby wejść w interakcję";

        _eventArea.BodyEntered += OnBodyEntered;
        _eventArea.BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is PlayerController)
        {
            _interactionLabel.Visible = true;
            _eventBus.PlayerEnteredSellerArea();
        }
    }

    private void OnBodyExited(Node3D body)
    {
        if (body is PlayerController)
        {
            _interactionLabel.Visible = false;
            _eventBus.PlayerExitedSellerArea();
        }
    }
}
