using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Signal] public delegate void PlayerAttackEventHandler(Vector2 spawnPosition, Vector2 direction);

	public const float Speed = 300.0f;

	public int CurrentHealth = 100;
	public int MaxHealth = 100;

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
			velocity = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
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
		CurrentHealth -= damage;
		Debug.Print("Player Health: " + CurrentHealth);
		if (CurrentHealth <= 0)
		{
			CurrentHealth = 0;
			Modulate = new Color(1, 0, 0);
			Debug.Print("Player is dead");
		}
	}
}
