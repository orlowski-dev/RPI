public class GetStartCharactersUseCase
    : IUseCase<GetStartCharactersRequest, GetStartCharactersResponse>
{
    public Result<GetStartCharactersResponse> Execute(GetStartCharactersRequest req)
    {
        return Result<GetStartCharactersResponse>.Success(
            new(
                ActorDefinitions: PlayerDefinitions.Values,
                PreviewImages: PlayerDefinitions.PreviewImages,
                TypeDescriptions: PlayerDefinitions.TypeDescripions,
                TypePlurals: PlayerDefinitions.TypePlural
            )
        );
    }
}
