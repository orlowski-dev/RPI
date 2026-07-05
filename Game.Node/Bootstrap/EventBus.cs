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

    public event Action? OnPlayerEnteredCamfire;

    public void PlayerEnteredCamfire()
    {
        OnPlayerEnteredCamfire?.Invoke();
    }

    public event Action? OnPlayerExitedCamfire;

    public void PlayerExitedCamfire()
    {
        OnPlayerExitedCamfire?.Invoke();
    }
}
