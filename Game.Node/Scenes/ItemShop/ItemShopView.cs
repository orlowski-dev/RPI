using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ItemShopView : Control
{
    private enum Containers
    {
        Shop,
        Inventory,
    }

    private enum Labels
    {
        PlayerInfo,
    }

    private enum Buttons
    {
        Close,
    }

    private Dictionary<Containers, Container> _containers = new();
    private Dictionary<Labels, Label> _labels = new();
    private Dictionary<Buttons, Button> _buttons = new();

    private IGameSessionProvider _gs = null!;
    private Inventory? _Inventory => _gs.Current?.Inventory;
    private ItemShopPresenter _isPrsenter = null!;
    private Player? _Player => _gs.Current?.Player;
    public Action? OnCloseAction;

    public override void _Ready()
    {
        _gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _isPrsenter = ServiceProviderHolder.Provider.GetRequiredService<ItemShopPresenter>();

        _containers[Containers.Shop] = GetNode<Container>("%SklItemsContainer");
        _containers[Containers.Inventory] = GetNode<Container>("%EkwItemsContainer");
        _labels[Labels.PlayerInfo] = GetNode<Label>("%PlayerInfo");
        _buttons[Buttons.Close] = GetNode<Button>("%CloseButton");

        _buttons[Buttons.Close].Pressed += () =>
        {
            OnCloseAction?.Invoke();
        };
    }

    public void InitUI()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_gs.Current is null || _Player is null)
        {
            DebugExtension.Fatal(this, "Current game session or Player is null!");
        }

        ClearContainer(_containers[Containers.Shop]);
        ClearContainer(_containers[Containers.Inventory]);
        _labels[Labels.PlayerInfo].Text = _Player.Info;

        SpawnButtonsInTab(_containers[Containers.Shop], _gs.Current.ShopItems);
        SpawnButtonsInTab(_containers[Containers.Inventory], _gs.Current.Inventory.Backpack.Items);
    }

    private void ClearContainer(Container container)
    {
        foreach (var child in container.GetChildren())
        {
            child.QueueFree();
        }
    }

    private Button SpawnItemButton(Item item, Container container)
    {
        var btn = GD.Load<PackedScene>(ScenePaths.InventoryItemButton).Instantiate<Button>();
        container.AddChild(btn);
        btn.Text = item.Name;
        return btn;
    }

    private void SpawnButtonsInTab(Container tab, List<Item> items)
    {
        if (_Player is null)
        {
            DebugExtension.Fatal(this, "Current game session is null!");
        }

        if (_Inventory is null)
        {
            DebugExtension.Fatal(this, "Current game session is null!");
        }

        // add items to the list
        foreach (var item in items)
        {
            var btn = SpawnItemButton(item, tab);
            btn.SetMeta("itemID", item.Id.ToString());

            var desc = item.GetInfo();

            var btnScript = (btn as InventoryItemButton)!;
            var price = ItemCatalog.CalcultePrice(item.Stats);
            var sellPrice = ItemCatalog.CalculateSellPrice(item.Stats);

            if (tab == _containers[Containers.Shop])
            {
                desc += $"Kup za {price}\n";
                var canAffort = _Player.Gold >= price;
                if (!canAffort)
                {
                    desc += "Nie masz wystarczająco golda";
                    btnScript.ThemeTypeVariation = "Invalid";
                }
                else
                {
                    btnScript.OnDoubleClick += () => OnShopItemDoubleClick(item);
                }
            }

            if (tab == _containers[Containers.Inventory])
            {
                desc += $"Sprzedaj za {sellPrice}\n";
                btnScript.OnDoubleClick += () => OnInventoryItemDoubleClick(item);
            }

            btn.TooltipText = desc;
        }
    }

    private void OnShopItemDoubleClick(Item item)
    {
        _isPrsenter.BuyItem(item);
        UpdateUI();
    }

    private void OnInventoryItemDoubleClick(Item item)
    {
        _isPrsenter.SellItem(item);
        UpdateUI();
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
