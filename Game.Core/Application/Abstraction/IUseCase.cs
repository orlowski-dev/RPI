namespace Game.Core.Application.Abstraction;

public interface IUseCase<in TRequest>
{
    public Result Execute(TRequest request);
}
