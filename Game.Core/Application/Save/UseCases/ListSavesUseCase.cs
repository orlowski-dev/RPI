public class ListSavesUseCase : IUseCase<ListSavesRequest, ListSavesResponse>
{
    public Result<ListSavesResponse> Execute(ListSavesRequest req)
    {
        var dto = new ListSavesResult();
        return Result<ListSavesResponse>.Success(new(dto));
    }
}
