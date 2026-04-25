using Godot;

public partial class CharacterCreatorController : Node
{
    private CharacterCreatorService _service;
    private CharacterCreatorSignals Signals => CharacterCreatorSignals.Instance;
    private CharacterCreatorData _data;

    public override void _Ready()
    {
        Signals.SetSelectedClass += OnSelectedClassChanged;

        _service = new CharacterCreatorService();
        SendData();
    }

    public override void _ExitTree()
    {
        Signals.SetSelectedClass -= OnSelectedClassChanged;
    }

    private void SendData()
    {
        _data = GetData();
        Signals.EmitDataSender(_data);
    }

    private CharacterCreatorData GetData()
    {
        return new(
            characterClasses: _service.DB.CharacterClasses,
            selectedClass: _service.SelectedClass
        );
    }

    private void OnSelectedClassChanged(string className)
    {
        _service.SelectedClass = className;
        SendData();
    }
}
