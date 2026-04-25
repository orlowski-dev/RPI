namespace Game.Tests.Shared;

public static class Shared
{
    public static PlayerCharacter GetNewPlayer()
    {
        var cc = new CharacterClass(
            name: "Warrior",
            hpBase: 100,
            attackBase: 50,
            defenseBase: 30,
            critBase: 1,
            luckBase: 10,
            maxHpBonus: 20,
            attackBonus: 3,
            defenseBonus: 3
        );
        return new PlayerCharacter(
            name: "Test Character",
            maxHp: cc.HpBase,
            attack: cc.AttackBase,
            defense: cc.AttackBase,
            luck: cc.AttackBase,
            critChance: cc.CritBase,
            characterClass: cc
        );
    }

    public static EnemyCharacter GetNewEnemyCharacter()
    {
        return new(
            name: "Test Enemy",
            maxHp: 100,
            attack: 50,
            defense: 30,
            critChance: 1,
            level: 3,
            enemyType: EnemyType.Normal
        );
    }
}
