using Godot;

public partial class Startup : Node
{
    public override void _Ready()
    {
        var provider = DependencyInjection.Build();
        ServiceProviderHolder.Init(provider);
        GD.Print("Bootstarp initilized.");

        // GetTree().ChangeSceneToFile(ScenePaths.MainMenu);
        GetTree().CallDeferred("change_scene_to_file", ScenePaths.MainMenu);
    }
}
