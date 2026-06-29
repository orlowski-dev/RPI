using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CityViewScript : Control
{
	private enum Pb
	{
		Hp,
		Exp,
	}

	private enum Lbl
	{
		PlayerName,
	}

	private CityPresenter _presenter = null!;

	private Dictionary<Pb, TextureProgressBar> _bars = null!;
	private Dictionary<Lbl, Label> _labels = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Player _player =>
		_gameSessionProvider.Current?.Player ?? throw new Exception("Player instance is null.");

	public override void _Ready()
	{
		_bars = new();
		_labels = new();
		_presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

		InitUI();
		UpdateUI();
	}

	private void InitUI()
	{
		_labels[Lbl.PlayerName] = GetNode<Label>("%L_PlayerName");
		_bars[Pb.Exp] = GetNode<TextureProgressBar>("%Pb_Exp");
		_bars[Pb.Hp] = GetNode<TextureProgressBar>("%Hb_CurrentHp");
	}

	private void UpdateUI()
	{
		_labels[Lbl.PlayerName].Text = _player.Name;
		_bars[Pb.Hp].MaxValue = _player.Stats.MaxHp;
		_bars[Pb.Hp].Value = _player.Stats.CurrentHp;
		_bars[Pb.Exp].MaxValue = _player.ExpNextLevel;
		_bars[Pb.Exp].Value = _player.Exp;
	}
}
