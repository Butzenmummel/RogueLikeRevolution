using System.Diagnostics;
using Godot;
using Scripts.Characters.Player;

public partial class PlayerStatsInterface : Control
{
	private const int PixelPerValue = 3;

	private PlayerVariables PlayerVariables => GetNode<PlayerVariables>("/root/PlayerVariables");

	private ProgressBar _healthBar;
	private ProgressBar _shieldBar;
	private ProgressBar _overheatBar;

	public override void _Ready()
	{
		_healthBar = GetNode<ProgressBar>("%HealthBar");
		_shieldBar = GetNode<ProgressBar>("%ShieldBar");
	}

	public override void _Process(double delta)
	{
		UpdateHealth();
		UpdateOverheat();
	}

	private void UpdateHealth()
	{
		_healthBar.MaxValue = PlayerVariables.MaxHealth;
		_healthBar.Value = PlayerVariables.CurrentHealth;

		var horizontalBarSize = PlayerVariables.MaxHealth * PixelPerValue;
		Mathf.Clamp(horizontalBarSize, 10 * PixelPerValue, 1800);
		_healthBar.CustomMinimumSize = new Vector2(horizontalBarSize, 20);
		_healthBar.GetNode<NinePatchRect>("Border").Size = new Vector2(horizontalBarSize + 16, 36);
		_healthBar.GetNode<Label>("Value").Size = new Vector2(horizontalBarSize, 36);
		_healthBar.GetNode<Label>("Value").Text = $"{PlayerVariables.CurrentHealth}";

		GetNode<VBoxContainer>("PlayerStatsContainer").UpdateMinimumSize();

		if (PlayerVariables.Shield <= 0)
		{
			_shieldBar.Hide();
		}
		else
		{
			_shieldBar.MaxValue = PlayerVariables.Shield;
			_shieldBar.Value = PlayerVariables.Shield;
			_shieldBar.CustomMinimumSize = new Vector2(PlayerVariables.Shield * PixelPerValue, 20);
			_shieldBar.GetNode<NinePatchRect>("Border").Size = new Vector2(PlayerVariables.Shield * PixelPerValue + 16, 36);
			_shieldBar.GetNode<Label>("Value").Size = new Vector2(PlayerVariables.Shield * PixelPerValue, 36);
			_shieldBar.GetNode<Label>("Value").Text = $"{PlayerVariables.Shield}";
			_shieldBar.Show();
		}
	}

	private void UpdateOverheat()
	{
	}
}
