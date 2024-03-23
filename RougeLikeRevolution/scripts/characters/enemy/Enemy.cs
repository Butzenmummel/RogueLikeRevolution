using Godot;
using System;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D
{
	[Signal] public delegate void PlayerShootEventHandler();

	public const float Speed = 150.0f;

	private Player _player;
	private NavigationAgent2D _navigationAgent;

	public override void _Ready()
	{
		_player = GetNode<Player>("/root/Main/Player");
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = ToLocal(_navigationAgent.GetNextPathPosition()).Normalized();
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private void MakePathToPlayer()
	{
		_navigationAgent.TargetPosition = _player.GlobalPosition;
	}

	public void OnTimerTimeout()
	{
		MakePathToPlayer();
	}

	public void OnAttackRangeBodyShapeEntered(Rid bodyRid, Node2D body, int bodyShape, int areaShape)
	{
		if (body.GetParent() is Player)
		{
			Debug.Print("Player is in attack range");
			var mousePosition = GetGlobalMousePosition();
			EmitSignal(nameof(PlayerShoot), mousePosition);
		}
	}

	public void OnAttackRangeBodyShapeExited(Rid bodyRid, Node2D body, int bodyShape, int areaShape)
	{
		if (body.GetParent() is Player)
		{
			Debug.Print("Player is out of attack range");
		}
	}
}
