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
		Description,
	}

	private enum Labels
	{
		Description,
		PlayerInfo,
		Armor,
		Weapon,
	}

	private enum Buttons
	{
		Close,
		Equip,
		n,
	}

	private Dictionary<Containers, Container> _containers = new();
	private Dictionary<Labels, Label> _labels = new();
	private Dictionary<Buttons, Button> _buttons = new();
	private Item? _selectedItem;

	public Action? OnCloseAction;

	public override void _Ready()
	{
		_gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		_containers[Containers.Items] = GetNode<Container>("%ItemsContainer");

		_labels[Labels.Description] = GetNode<Label>("%ItemDesc");
		_labels[Labels.PlayerInfo] = GetNode<Label>("%PlayerInfo");
		_labels[Labels.Armor] = GetNode<Label>("%ArmorLabel");
		_labels[Labels.Weapon] = GetNode<Label>("%WeaponLabel");
		_containers[Containers.Description] = GetNode<Container>("%DescContainer");

		_buttons[Buttons.Close] = GetNode<Button>("%CloseButton");
		_buttons[Buttons.Equip] = GetNode<Button>("%EquipButton");

		_buttons[Buttons.Equip].Pressed += OnEquipClick;

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

		if (_selectedItem is null)
		{
			_containers[Containers.Description].Visible = false;
		}

		_labels[Labels.Armor].Text = _Inventory.Equipment?.Armor is not null
			? _Inventory.Equipment.Armor.Name
			: "";
		_labels[Labels.Weapon].Text = _Inventory.Equipment?.Weapon is not null
			? _Inventory.Equipment.Weapon.Name
			: "";

		_labels[Labels.PlayerInfo].Text = _Player.Info;

		// clear item list
		foreach (var child in _containers[Containers.Items].GetChildren())
		{
			child.QueueFree();
		}

		// add items to the list
		foreach (var item in _Inventory.Backpack.Items)
		{
			if (item == _Inventory.Equipment?.Armor || item == _Inventory.Equipment?.Weapon)
			{
				return;
			}

			var itemLabel = GD.Load<PackedScene>(ScenePaths.ItemLabel).Instantiate<Button>();
			itemLabel.Text = item.Name;

			_containers[Containers.Items].AddChild(itemLabel);
			itemLabel.SetMeta("itemID", item.Id.ToString());

			itemLabel.Pressed += () => OnItemSelect(item);
		}
	}

	private void OnItemSelect(Item item)
	{
		if (_Player is null)
		{
			DebugExtension.Fatal(this, "Player is null!");
		}

		_selectedItem = item;
		var desc = item.GetInfo();
		if (!item.AllowedClasses.Any((cl) => cl == _Player.Type))
		{
			desc += "Nie można użyć tego przedmiotu";
		}

		_labels[Labels.Description].Text = desc;
		_containers[Containers.Description].Visible = true;

		UpdateUI();
	}

	private void OnEquipClick()
	{
		if (_selectedItem is null || _Player is null)
		{
			DebugExtension.Fatal(this, "selected item or player is null");
		}

		_Inventory?.Equipment.Equip(_selectedItem, _Player.Type);
		_selectedItem = null;
		UpdateUI();
	}

	public void SetVisible()
	{
		UpdateUI();
		Visible = true;
	}

	public void SetInvisible()
	{
		_selectedItem = null;
		Visible = false;
	}
}
