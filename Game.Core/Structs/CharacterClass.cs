/// <summary>
/// Struktura przechowująca definicję klasy postaci wraz z bazowymi statystykami i bonusami przy awansie poziomu.
/// </summary>
public readonly struct CharacterClass
{
    public string Name { get; init; }
    public int HpBase { get; init; }
    public int AttackBase { get; init; }
    public int DefenseBase { get; init; }
    public int CritBase { get; init; }
    public int LuckBase { get; init; }

    public string ClassIconName { get; init; }
    public string NodeName { get; init; }

    // bonusy to o ile się zwiększają dane statyski przy levelUp
    public int HpBonus { get; init; }
    public int AttackBonus { get; init; }
    public int DefenseBonus { get; init; }

    public CharacterClass(
        string name,
        int hpBase,
        int attackBase,
        int defenseBase,
        int critBase,
        int luckBase,
        int hpBonus,
        int attackBonus,
        int defenseBonus,
        string? classIconName = null,
        string? nodeName = null
    )
    {
        Name = name;
        HpBase = hpBase;
        AttackBase = attackBase;
        DefenseBase = defenseBase;
        CritBase = critBase;
        LuckBase = luckBase;
        HpBonus = hpBonus;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        ClassIconName = classIconName ?? string.Empty;
        NodeName = nodeName ?? string.Empty;
    }
}
