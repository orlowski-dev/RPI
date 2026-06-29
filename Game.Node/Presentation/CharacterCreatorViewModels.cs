public record CharacterCreatorInitViewModel(GetStartCharactersResponse? Data, Error? Error = null);

public record CharacterCreatorNewGameViewModel(GameSession? GameSession, Error? Error = null);
