public class PlayerFactory
{
    public Player Create(string name, PlayerType playerType)
    {
        var def = PlayerDefinitions.Values[playerType];
        return new Player(name: name, stats: def.BaseStats, type: playerType);
    }
}
