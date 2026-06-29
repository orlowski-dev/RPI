public class EnemyFactory
{
    private Random _random = new Random();

    /// <summary>
    /// zwracam randomowego enemy z definicji
    /// </summary>
    public Enemy Create()
    {
        var type = GetRandomType();
        var def = EnemyDefinitions.Values[type].Enemy;
        return def;
    }

    private EnemyType GetRandomType()
    {
        var values = Enum.GetNames(typeof(EnemyType));
        return (EnemyType)_random.Next(0, values.Length);
    }
}
