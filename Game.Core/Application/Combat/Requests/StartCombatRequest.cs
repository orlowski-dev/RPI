namespace Game.Core.Application.Combat.Requests;

public record StartCombatRequest(Player Player, IEnumerable<Enemy> Enemies);
