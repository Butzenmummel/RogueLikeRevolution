using Godot;
using System;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 200.0f;
	private PlayerController _player;

	public override void _Ready()
	{
		_player = GetNode<PlayerController>("/root/Main/Player");
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Position.DirectionTo(_player.Position) * Speed;
		MoveAndSlide();
	}
}
