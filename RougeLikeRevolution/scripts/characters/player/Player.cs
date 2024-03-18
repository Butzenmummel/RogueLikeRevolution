using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;

	public int CurrentHealth = 100;
	public int MaxHealth = 100;

	private AnimatedSprite2D _playerSprite;

	public override void _Ready()
	{
		_playerSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
	}

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer();
		AnimatePlayer();
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

	private void AnimatePlayer()
	{
		if (Input.IsActionPressed("move_left"))
		{
			_playerSprite.FlipH = true;
		}
		else if (Input.IsActionPressed("move_right"))
		{
			_playerSprite.FlipH = false;
		}

		if (Input.IsActionPressed("move_up"))
		{
			_playerSprite.Animation = "idle_back";
		}
		else if (Input.IsActionPressed("move_down"))
		{
			_playerSprite.Animation = "idle_front";
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("zoom_in") && GetNode<Camera2D>("PlayerCamera").Zoom.X < 2.0f)
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
