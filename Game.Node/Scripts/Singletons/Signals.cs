using Godot;

/// <summary>
/// Globalny EventBus aplikacji.
/// </summary>
/// <remarks>
/// Centralny system komunikacji pomiędzy modelami i kontrolerami.
/// </remarks>
[GlobalClass]
public partial class Signals : BaseSingleton<Signals>, ISignals
{
    [Signal]
    public delegate void SetGameStateEventHandler(GameState newState);

    [Signal]
    public delegate void CharacterHpChangedEventHandler(int hp);

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    /// <summary>
    /// Emituje sygnał zmiany stanu gry.
    /// </summary>
    /// <param name="newState">Nowy stan</param>
    public void EmitSetGameState(GameState newState)
    {
        EmitSignal(SignalName.SetGameState, (int)newState);
    }

    /// <summary>
    /// Emituje sygnał zmiany wartości hp postaci.
    /// </summary>
    /// <param name="newState">Nowy stan</param>
    public void EmitSetCharacterHpChanged(int newHp)
    {
        EmitSignal(SignalName.CharacterHpChanged, newHp);
    }
}
