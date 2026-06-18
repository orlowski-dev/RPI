public class ListSavesUseCase : IUseCase<ListSavesRequest, ListSavesResponse>
{
    public Result<ListSavesResponse> Execute(ListSavesRequest req)
    {
        var list = new JsonSaveRepository().List();
        if (list.Count == 0)
        {
            return Result<ListSavesResponse>.Fail(new("No save data found.", ErrorType.NotFound));
        }

        return Result<ListSavesResponse>.Success(new(list));
    }
}
