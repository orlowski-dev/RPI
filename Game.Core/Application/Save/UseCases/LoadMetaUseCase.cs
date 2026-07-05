public class LoadMetaUseCase : IUseCase<LoadMetaRequest, LoadMetaResponse>
{
    private readonly JsonSaveRepository _saveRepo;

    public LoadMetaUseCase(JsonSaveRepository saveRepo)
    {
        _saveRepo = saveRepo;
    }

    public Result<LoadMetaResponse> Execute(LoadMetaRequest req)
    {
        var metaRes = _saveRepo.LoadMeta();
        MetaSnapshot? metaSnap = null;

        if (metaRes.IsSuccess)
        {
            metaSnap = metaRes.Value;
        }

        return Result<LoadMetaResponse>.Success(new(metaSnap));
    }
}
