using Godot;

public partial class InventoryItemButton : Button
{
	public Action? OnDoubleClick;

	public override void _Ready()
	{
		GuiInput += OnButtonGuiInput;
	}

	private void OnButtonGuiInput(InputEvent @event)
	{
		// dwuklik
		if (
			@event is InputEventMouseButton mouseEvent
			&& mouseEvent.ButtonIndex == MouseButton.Left
			&& mouseEvent.Pressed
			&& mouseEvent.DoubleClick
		)
		{
			OnDoubleClick?.Invoke();
		}
	}
}
