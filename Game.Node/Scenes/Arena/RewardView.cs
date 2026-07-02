using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class RewardView : Control
{
    private enum EnLabels
    {
        RewardExp,
        RewardGold,
        RewardItems,
    }

    private IGameSessionProvider _gs = null!;
    private Dictionary<EnLabels, Label> _labels = new();
    private Button _okButton = null!;

    public override void _Ready()
    {
        _gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        Visible = false;

        _labels[EnLabels.RewardExp] = GetNode<Label>("%RewExp");
        _labels[EnLabels.RewardGold] = GetNode<Label>("%RewGold");
        _labels[EnLabels.RewardItems] = GetNode<Label>("%RewItems");
        _okButton = GetNode<Button>("%OkButton");

        _okButton.Pressed += OnOkButtonClicked;
    }

    public void ShowView()
    {
        var reward = _gs.Current?.CombatSession?.Reward;
        if (reward is null)
        {
            DebugExtension.Fatal(this, "Reward is null!");
            return;
        }

        _labels[EnLabels.RewardExp].Text = reward.Experience + "";
        _labels[EnLabels.RewardGold].Text = reward.Gold + "";
        _labels[EnLabels.RewardItems].Text = string.Join(
            ',',
            reward.Items.Select((item) => item.Name)
        );

        if (reward.ExpBonus > 0)
        {
            _labels[EnLabels.RewardExp].Text += $" + bonus {reward.ExpBonus}";
        }

        if (reward.GoldBonus > 0)
        {
            _labels[EnLabels.RewardGold].Text += $" + bonus {reward.GoldBonus}";
        }

        Visible = true;
    }

    private void OnOkButtonClicked()
    {
        GetTree().CallDeferred("change_scene_to_file", ScenePaths.DungeonTest);
    }
}
