using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class InventoryView : Control
{
	private IGameSessionProvider _gs = null!;
	private Inventory? _Inventory => _gs.Current?.Inventory;
	private Equipment? _Equipment => _gs.Current?.Inventory.Equipment;
	private Player? _Player => _gs.Current?.Player;

	private enum Containers
	{
		Items,
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

	public Action? OnCloseAction;

	public override void _Ready()
	{
		_gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		_containers[Containers.Items] = GetNode<Container>("%ItemsContainer");

		_labels[Labels.PlayerInfo] = GetNode<Label>("%PlayerInfo");

		_buttons[Buttons.Close] = GetNode<Button>("%CloseButton");

		_buttons[Buttons.Close].Pressed += () =>
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

		_labels[Labels.PlayerInfo].Text = _Player.Info;

		// clear item list
		ClearSlot(_containers[Containers.Items]);

		// add items to the list
		foreach (var item in _Inventory.Backpack.Items)
		{
			var btn = SpawnItemButton(item, _containers[Containers.Items]);
			var equiped = false;

			if (item == _Inventory.Equipment?.Armor || item == _Inventory.Equipment?.Weapon)
			{
				btn.ThemeTypeVariation = "Equiped";
				equiped = true;
			}

			btn.SetMeta("itemID", item.Id.ToString());

			var desc = item.GetInfo();
			if (!item.AllowedClasses.Contains(_Player.Type))
			{
				btn.ThemeTypeVariation = "Invalid";
				desc += "Nie można użyć tego przedmiotu";
			}

			btn.TooltipText = desc;

			var btnScript = (btn as InventoryItemButton)!;
			btnScript.OnDoubleClick = () => OnItemSelect(item, equiped);
		}
	}

	private void ClearSlot(Container container)
	{
		foreach (var child in container.GetChildren())
		{
			child.QueueFree();
		}
	}

	private void OnItemSelect(Item item, bool equiped)
	{
		if (_Player is null)
		{
			DebugExtension.Fatal(this, "Player is null!");
		}

		if (!equiped)
		{
			_Inventory?.Equipment.Equip(item, _Player.Type);
		}
		else
		{
			_Inventory?.Equipment.Remove(item.Id);
		}

		UpdateUI();
		GD.Print($"Załóżono {item.Name}");
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

	private Button SpawnItemButton(Item item, Container container)
	{
		var btn = GD.Load<PackedScene>(ScenePaths.InventoryItemButton).Instantiate<Button>();
		container.AddChild(btn);
		btn.Text = item.Name;
		return btn;
	}
}
