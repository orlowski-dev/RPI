using Godot;

/// <summary>
/// Globalny EventBus aplikacji.
/// </summary>
/// <remarks>
/// Centralny system komunikacji pomiędzy modelami i kontrolerami.
/// </remarks>
public partial class Signals : BaseSingleton<Signals>, ISignals
{
    [Signal]
    public delegate void CharacterHpChangedEventHandler(int hp);

    [Signal]
    public delegate void GameStateChangedEventHandler(GameManagerData data);

    /// <summary>
    /// Emituje sygnał zmiany wartości hp postaci.
    /// </summary>
    /// <param name="newState">Nowy stan</param>
    public void EmitCharacterHpChanged(int newHp)
    {
        EmitSignal(SignalName.CharacterHpChanged, newHp);
    }

    public void EmitGameStateChanged(IGameManagerData data)
    {
        EmitSignal(SignalName.GameStateChanged, (GameManagerData)data);
    }
}
