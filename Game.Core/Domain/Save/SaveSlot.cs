namespace Game.Core.Domain.Save;

// co jest zapisywane
public class SaveSlot
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastPlayed { get; }

    public SaveSlot()
    {
        LastPlayed = DateTime.Now;
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
}
