public class ItemStatsMapper : IItemStatsSnapshotMapper
{
    public ItemStatsSnapshot ToSnapshot(ItemStats itemStats)
    {
        return new(
            MaxHp: itemStats.MaxHp,
            Attack: itemStats.Attack,
            Defense: itemStats.Defense,
            CriticalChance: itemStats.CriticalChance,
            Luck: itemStats.Luck
        );
    }

    public ItemStats Restore(ItemStatsSnapshot snapshot)
    {
        return new(
            MaxHp: snapshot.MaxHp,
            Attack: snapshot.Attack,
            Defense: snapshot.Defense,
            CriticalChance: snapshot.CriticalChance,
            Luck: snapshot.Luck
        );
    }
}
