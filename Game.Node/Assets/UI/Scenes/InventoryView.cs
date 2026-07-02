using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class InventoryView : Control
{
    private IGameSessionProvider _gs = null!;
    private Inventory? _Inventory => _gs.Current?.Inventory;
    private Player? _Player => _gs.Current?.Player;

    private enum Containers
    {
        Items,
    }

    private Dictionary<Containers, Container> _containers = new();

    public Action? OnCloseAction;
    private Button _closeBtn = null!;

    public override void _Ready()
    {
        _gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

        _containers[Containers.Items] = GetNode<Container>("%ItemsContainer");
        _closeBtn = GetNode<Button>("%CloseButton");
        _closeBtn.Pressed += () =>
        {
            OnCloseAction?.Invoke();
        };
    }

    private void UpdateUI()
    {
        if (_Inventory is null || _Player is null)
        {
            DebugExtension.Fatal(this, "Inventory or Player is null!");
            return;
        }

        // clear item list
        foreach (var child in _containers[Containers.Items].GetChildren())
        {
            child.QueueFree();
        }

        // add items to the list
        foreach (var item in _Inventory.Backpack.Items)
        {
            var itemLabel = GD.Load<PackedScene>(ScenePaths.ItemLabel).Instantiate<Button>();
            itemLabel.Text = item.Name;
            if (!item.AllowedClasses.Any((cl) => cl == _Player.Type))
            {
                itemLabel.Text += " (Nie można użyć)";
            }

            _containers[Containers.Items].AddChild(itemLabel);
        }
    }

    public void SetVisible()
    {
        UpdateUI();
        Visible = true;
    }

    public void SetInvisible()
    {
        Visible = false;
    }
}
