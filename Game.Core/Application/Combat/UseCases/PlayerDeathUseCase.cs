namespace Game.Core.Application.Combat.UseCases;

public class PlayerDeathUseCase : IUseCase<PlayerDeathRequest, PlayerDeathResponse>
{
    public Result<PlayerDeathResponse> Execute(PlayerDeathRequest req)
    {
        // będzie trzeba zabrać trochę golda czy coś
        return Result<PlayerDeathResponse>.Success(new());
    }
}
