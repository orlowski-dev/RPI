public partial class CityPresenter
{
    private readonly GenerateShopItemsUseCase _generateUseCase;
    private readonly SaveGameUseCase _saveGameUC;

    public CityPresenter(GenerateShopItemsUseCase generateUseCase, SaveGameUseCase saveGameUseCase)
    {
        _generateUseCase = generateUseCase;
        _saveGameUC = saveGameUseCase;
    }

    public void OnViewLoad()
    {
        var res = _generateUseCase.Execute(new());
    }

    public void OnSaveGame()
    {
        _saveGameUC.Execute(new());
    }
}
