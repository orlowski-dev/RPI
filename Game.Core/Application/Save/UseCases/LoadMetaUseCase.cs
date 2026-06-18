public class LoadMetaUseCase : IUseCase<LoadMetaRequest, LoadMetaResponse>
{
    public Result<LoadMetaResponse> Execute(LoadMetaRequest req)
    {
        var metaRes = new JsonSaveRepository().LoadMeta();
        MetaSnapshot? metaSnap = null;

        if (metaRes.IsSuccess)
        {
            metaSnap = metaRes.Value;
        }

        return Result<LoadMetaResponse>.Success(new(metaSnap));
    }
}
