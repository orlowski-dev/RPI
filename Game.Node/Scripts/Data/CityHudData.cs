using Godot;

public partial class CityHudData : GodotObject
{
    public PlayerCharacter PlayerCharacter { get; init; }

    public CityHudData(PlayerCharacter playerCharacter)
    {
        PlayerCharacter = playerCharacter;
    }
}
