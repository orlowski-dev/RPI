using Godot;

public partial class EnemyStatsListItem : Control
{
	private int _hp;
	private Label _nameLabel = null!;
	private Label _hpLabel = null!;
	private TextureProgressBar _hpBar = null!;

	public int CurrentEnemyHP
	{
		get { return _hp; }
		set
		{
			_hp = value;
			_hpLabel.Text = $"{_hp} / {_hpBar.MaxValue}";
			_hpBar.Value = _hp;
		}
	}

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%EnemyName");
		_hpLabel = GetNode<Label>("%EnemyHP");
		_hpBar = GetNode<TextureProgressBar>("%EnemyHealthBar");
	}

	public void Init(string name, int maxHp)
	{
		_nameLabel.Text = name;
		_hpBar.MaxValue = maxHp;
	}
}
