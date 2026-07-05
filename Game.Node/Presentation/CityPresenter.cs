public partial class CityPresenter
{
    private readonly GenerateShopItemsUseCase _generateUseCase;

    public CityPresenter(GenerateShopItemsUseCase generateUseCase)
    {
        _generateUseCase = generateUseCase;
    }

    public void OnViewLoad()
    {
        var res = _generateUseCase.Execute(new());
    }
}
