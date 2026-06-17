namespace Game.Core.Infrastructure.Save.Abstraction;

public interface ISaveRepository
{
    public void Save();
    public void Load();
    public void List();
}
