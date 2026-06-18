public class MetaMapper : IMetaSnapshotMapper
{
    public MetaSnapshot ToSnapshot(Guid lastSessionId)
    {
        return new(CreatedAt: DateTime.Now, LastSessionId: lastSessionId);
    }

    public Guid Restore(MetaSnapshot metaSnapshot)
    {
        return metaSnapshot.LastSessionId;
    }
}
