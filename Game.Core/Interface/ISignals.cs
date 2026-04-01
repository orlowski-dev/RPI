/// /// <summary>
/// Interfejs sygnałów globalnych
/// </summary>
/// <remarks>
/// Unikanie zależności do Godot:Node
/// </remarks>
public interface ISignals
{
    public delegate void SetGameStateEventHandler(GameState newState);
    public delegate void CharacterHpChangedEventHandler(int hp);

    public void EmitSetGameState(GameState newState);
    public void EmitSetCharacterHpChanged(int newHp);
}
