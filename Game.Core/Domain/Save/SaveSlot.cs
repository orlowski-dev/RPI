namespace Game.Core.Domain.Save;

public class SaveSlot
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastPlayed { get; }
}
