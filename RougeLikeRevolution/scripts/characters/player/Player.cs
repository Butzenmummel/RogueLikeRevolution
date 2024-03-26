using Godot;
using Scripts.Characters.Player;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Signal] public delegate void PlayerAttackEventHandler(Vector2 spawnPosition, Vector2 direction);

	private PlayerVariables PlayerVariables => GetNode<PlayerVariables>("/root/PlayerVariables");

	public override void _Ready()
	{
		Connect(nameof(PlayerAttack), new Callable(GetNode<ProjectilesManager>("/root/Main/Managers/ProjectilesManager"), "SpawnProjectile"));
	}

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer();
		Modulate = new Color(1, 1, 1);
	}

	private void MovePlayer()
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * PlayerVariables.Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, PlayerVariables.Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, PlayerVariables.Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			var targetPosition = GetGlobalMousePosition();
			EmitSignal(nameof(PlayerAttack), GlobalPosition, targetPosition - GlobalPosition);
		}
		else if (@event.IsActionPressed("zoom_in") && GetNode<Camera2D>("PlayerCamera").Zoom.X < 2.0f)
		{
			GetNode<Camera2D>("PlayerCamera").Zoom += new Vector2(0.1f, 0.1f);
		}
		else if (@event.IsActionPressed("zoom_out") && GetNode<Camera2D>("PlayerCamera").Zoom.X > 1f)
		{
			GetNode<Camera2D>("PlayerCamera").Zoom -= new Vector2(0.1f, 0.1f);
		}
	}

	public void OnPlayerReceivedDamage(int damage)
	{
		PlayerVariables.ChangeCurrentHealth(-damage);
		Debug.Print("Player Health: " + PlayerVariables.CurrentHealth);
		if (PlayerVariables.CurrentHealth <= 0)
		{
			PlayerVariables.CurrentHealth = 0;
			Modulate = new Color(1, 0, 0);
			Debug.Print("Player is dead");
		}
	}
}
