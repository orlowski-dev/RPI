public partial class CharacterCreatorService
{
    public CharacterClassesDB DB { get; init; } = new CharacterClassesDB();
    public string SelectedClass { get; set; } = String.Empty;

    public CharacterCreatorService()
    {
        SelectedClass = "warrior";
    }
}
