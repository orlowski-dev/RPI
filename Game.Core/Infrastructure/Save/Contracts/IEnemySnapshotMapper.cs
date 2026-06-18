public interface IEnemySnapshotMapper
{
    EnemySnapshot ToSnapshot(Enemy enemy);
    Enemy Restore(EnemySnapshot enemySnapshot);
}
