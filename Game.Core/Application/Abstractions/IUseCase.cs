namespace Game.Core.Application.Abstractions;

public interface IUseCase<in TRequest>
{
    public Result Execute(TRequest request);
}
