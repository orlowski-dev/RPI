namespace Game.Core.Application.Save.UseCases;

public class SaveGameUseCase : IUseCase<SaveGameRequest, SaveGameResponse>
{
    public Result<SaveGameResponse> Execute(SaveGameRequest req)
    {
        return Result<SaveGameResponse>.Success(new());
    }
}
