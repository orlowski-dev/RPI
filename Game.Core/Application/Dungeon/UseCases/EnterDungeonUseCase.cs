public class EnterDungeonUseCase : IUseCase<EnterDungeonRequest, EnterDungeonResponse>
{
    public Result<EnterDungeonResponse> Execute(EnterDungeonRequest req)
    {
        Dungeon dungeon = default!;

        if (req.GameSession.Dungeon is null)
        {
            dungeon = new DungeonFactory().Create();
            req.GameSession.EnterDungeon(dungeon);
        }
        else
        {
            dungeon = req.GameSession.Dungeon;
        }

        return Result<EnterDungeonResponse>.Success(new(dungeon));
    }
}
