namespace Game.Core.Application.Combat;

public record StartCombatRequest(CombatParticipant Player, IEnumerable<CombatParticipant> Enemies);
