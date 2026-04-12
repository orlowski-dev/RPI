/// /// <summary>
/// Interfejs sygnałów globalnych
/// </summary>
/// <remarks>
/// Unikanie zależności do Godot:Node
/// </remarks>
public interface ISignals
{
    public delegate void CharacterHpChangedEventHandler(int hp);
    public delegate void GameStateChangedEventHandler(IGameManagerData data);

    public void EmitCharacterHpChanged(int newHp);
    public void EmitGameStateChanged(IGameManagerData data);
}
