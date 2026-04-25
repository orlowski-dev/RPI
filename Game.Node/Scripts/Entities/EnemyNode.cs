using Godot;

public partial class EnemyNode : CharacterBody2D
{
    private EnemyCharacter _stats;

    public override void _Ready() { }

    public void Init(EnemyCharacter stats)
    {
        _stats = stats;
    }

    public EnemyCharacter Stats => _stats;
}
