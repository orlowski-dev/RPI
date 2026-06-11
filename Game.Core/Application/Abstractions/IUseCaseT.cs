namespace Game.Core.Application.Abstractions;

public interface IUseCase<in TRequest, TResponse>
{
    public Result<TResponse> Execute(TRequest request);
}
