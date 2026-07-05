public class ItemShopPresenter
{
    private readonly BuyItemUseCase _buyUC;
    private readonly SellItemUseCase _sellUC;

    public ItemShopPresenter(BuyItemUseCase buyUC, SellItemUseCase sellUC)
    {
        _buyUC = buyUC;
        _sellUC = sellUC;
    }

    public void BuyItem(Item item)
    {
        _buyUC.Execute(new BuyItemRequest(Item: item));
    }

    public void SellItem(Item item)
    {
        _sellUC.Execute(new SellItemRequest(Item: item));
    }
}
