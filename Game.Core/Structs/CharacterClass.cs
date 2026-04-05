public readonly struct CharacterClass
{
    public string Name { get; init; }
    public int HpBonus { get; init; }
    public int AttackBonus { get; init; }
    public int DefenseBonus { get; init; }

    public CharacterClass(string name, int hpBonus, int attackBonus, int defenseBonus)
    {
        Name = name;
        HpBonus = hpBonus;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
    }
}
