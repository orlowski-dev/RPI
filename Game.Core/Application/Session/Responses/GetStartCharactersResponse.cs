public record GetStartCharactersResponse(
    Dictionary<PlayerType, ActorDefinition> ActorDefinitions,
    Dictionary<PlayerType, string> PreviewImages,
    Dictionary<PlayerType, string> TypeDescriptions,
    Dictionary<PlayerType, string> TypePlurals
);
