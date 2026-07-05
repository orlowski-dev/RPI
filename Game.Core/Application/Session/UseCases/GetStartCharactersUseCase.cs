public class GetStartCharactersUseCase
    : IUseCase<GetStartCharactersRequest, GetStartCharactersResponse>
{
    public Result<GetStartCharactersResponse> Execute(GetStartCharactersRequest req)
    {
        var def = PlayerDefinitions.Values;
        return Result<GetStartCharactersResponse>.Success(
            new GetStartCharactersResponse(PlayerDefinitions: def)
        );
    }
}
