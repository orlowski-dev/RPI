using Microsoft.Extensions.DependencyInjection;

public class CharacterCreatorPresenter
{
    private readonly StartNewGameUseCase _startNewGame = null!;
    private readonly GetStartCharactersUseCase _getCharacters;

    public CharacterCreatorPresenter()
    {
        _startNewGame = ServiceProviderHolder.Provider.GetRequiredService<StartNewGameUseCase>();
        _getCharacters =
            ServiceProviderHolder.Provider.GetRequiredService<GetStartCharactersUseCase>();
    }

    public CharacterCreatorInitViewModel OnViewReady()
    {
        // var res = new GetStartCharactersUseCase().Execute(new());
        // var data = res.Value;
        // return new(Data: data);
        var data = _getCharacters.Execute(new()).Value;
        return new(Data: data);
    }

    public CharacterCreatorNewGameViewModel OnNewGame(string playerName, PlayerType playerType)
    {
        var res = _startNewGame.Execute(new(playerName, playerType));
        if (res.IsFailure)
        {
            return new(GameSession: null, Error: new("Response failure.", ErrorType.Validation));
        }

        return new(GameSession: res.Value.GameSession);
    }
}
