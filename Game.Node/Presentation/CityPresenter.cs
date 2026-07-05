public partial class CityPresenter
{
    private readonly GenerateShopItemsUseCase _generateUseCase;
    private readonly SaveGameUseCase _saveGameUC;
    private readonly ResetDungeonUseCase _resetDungUC;

    public CityPresenter(
        GenerateShopItemsUseCase generateUseCase,
        SaveGameUseCase saveGameUseCase,
        ResetDungeonUseCase resetDungeonUseCase
    )
    {
        _generateUseCase = generateUseCase;
        _saveGameUC = saveGameUseCase;
        _resetDungUC = resetDungeonUseCase;
    }

    public void OnViewLoad()
    {
        _resetDungUC.Execute(new());
        var res = _generateUseCase.Execute(new());
    }

    public void OnSaveGame()
    {
        _saveGameUC.Execute(new());
    }
}
