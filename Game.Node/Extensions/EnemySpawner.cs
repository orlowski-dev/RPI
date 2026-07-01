using Godot;

public static class EnemySpawner
{
    private const string EnemyScenePath = "res://Scenes/Enemy/Enemy.tscn";

    public static EnemyScript GetNode(string scenePath)
    {
        var enemyNode = GD.Load<PackedScene>(EnemyScenePath).Instantiate<EnemyScript>();
        var enemyModel = GD.Load<PackedScene>(scenePath).Instantiate<Node3D>();
        enemyNode.AddChild(enemyModel);
        enemyModel.Name = "Model";
        enemyModel.Position = Vector3.Zero;
        return enemyNode;
    }
}
