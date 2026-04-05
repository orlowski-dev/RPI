namespace Game.Tests.Shared;

public static class Shared
{
    public static PlayerCharacter GetNewPlayer()
    {
        return new PlayerCharacter(
            name: "Test Character",
            maxHp: 100,
            attack: 50,
            defense: 30,
            luck: 10,
            critChance: 1,
            characterClass: new CharacterClass(
                name: "Warrior",
                hpBonus: 20,
                attackBonus: 3,
                defenseBonus: 3
            )
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
