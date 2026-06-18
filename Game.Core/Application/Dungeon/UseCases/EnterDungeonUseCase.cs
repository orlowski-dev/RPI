public class EnterDungeonUseCase : IUseCase<EnterDungeonRequest, EnterDungeonResponse>
{
    public Result<EnterDungeonResponse> Execute(EnterDungeonRequest req)
    {
        return Result<EnterDungeonResponse>.Success(new());
    }
}
