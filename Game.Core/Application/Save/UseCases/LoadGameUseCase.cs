public class LoadGameUseCase : IUseCase<LoadGameRequest, LoadGameResponse>
{
    public Result<LoadGameResponse> Execute(LoadGameRequest req)
    {
        var dto = new LoadGameResult();
        return Result<LoadGameResponse>.Success(new(dto));
    }
}
