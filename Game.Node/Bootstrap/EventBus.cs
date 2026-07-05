public class EventBus
{
    public event Action? OnPlayerEnteredSellerArea;
    public event Action? OnPlayerExitedSellerArea;

    public void PlayerEnteredSellerArea()
    {
        OnPlayerEnteredSellerArea?.Invoke();
    }

    public void PlayerExitedSellerArea()
    {
        OnPlayerExitedSellerArea?.Invoke();
    }
}
