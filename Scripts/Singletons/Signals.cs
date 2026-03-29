using System;
using Godot;

/// <summary>
/// Globalny EventBus
/// </summary>
public partial class Signals : Node
{
    [Signal]
    public delegate void SetGameStateEventHandler(GameState newState);

    public static Signals Instance { get; private set; }

    public override void _EnterTree()
    {
        if (Instance != null)
        {
            GD.PushError("Signals już istnieje!");
            QueueFree();
            return;
        }

        Instance = this;
    }

    public void EmitSetGameState(GameState newState)
    {
        EmitSignal(SignalName.SetGameState, (int)newState);
    }
}
