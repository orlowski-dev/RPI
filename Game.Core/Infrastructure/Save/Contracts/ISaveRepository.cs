namespace Game.Core.Infrastructure.Save.Contracts;

public interface ISaveRepository
{
    public void Save();
    public void Load();
    public void List();
}
