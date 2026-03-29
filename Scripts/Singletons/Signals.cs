using System;
using Godot;

/// <summary>
/// Globalny EventBus
/// </summary>
public partial class Signals : BaseSingleton<Signals>
{
    [Signal]
    public delegate void SetGameStateEventHandler(GameState newState);

    // public static Signals Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public void EmitSetGameState(GameState newState)
    {
        EmitSignal(SignalName.SetGameState, (int)newState);
    }
}
