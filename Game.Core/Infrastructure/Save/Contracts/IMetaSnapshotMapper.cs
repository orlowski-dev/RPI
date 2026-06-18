public interface IMetaSnapshotMapper
{
    MetaSnapshot ToSnapshot(Guid LastSessionId);
    Guid Restore(MetaSnapshot metaSnapshot);
}
