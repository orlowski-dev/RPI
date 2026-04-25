using System.Collections.Generic;
using Godot;

public partial class CharacterCreatorData : GodotObject
{
    public Dictionary<string, CharacterClass> CharacterClasses { get; init; }
    public string SelectedClass { get; init; }

    public CharacterCreatorData(
        Dictionary<string, CharacterClass> characterClasses,
        string selectedClass
    )
    {
        CharacterClasses = characterClasses;
        SelectedClass = selectedClass;
    }
}
