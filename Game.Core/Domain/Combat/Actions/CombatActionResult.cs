public record CombatActionResult(
    Actor Attacker, // kto wykonał akcję
    Actor Target, // na kim
    int Value, // obrażenia, leczenie itd
    ActionType Type,
    string Message // gotowy tekst do UI
);
