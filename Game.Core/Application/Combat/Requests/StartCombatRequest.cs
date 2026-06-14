namespace Game.Core.Application.Combat.Requests;

public record StartCombatRequest(CombatParticipant Player, IEnumerable<CombatParticipant> Enemies);
